
using Mapster;
using Pakiza.Application.Common.Interfaces;
using Pakiza.Application.DTOs.Token;
using Pakiza.Application.DTOs.User;
using Pakiza.Application.Repositories.Users;

namespace Pakiza.Infrastructure.Services.Tokens;

public class TokenService : ITokenService
{
    private readonly IUserRepository _iUserRepository;
    private readonly IUsersTokenRepository _iUsersTokenRepository;
    private readonly ILogger<TokenService> _iLogger;
    private readonly JWTSettings _jwtSetting;
    private readonly Claims _claims;
    public TokenService(IUserRepository iUserRepository, IUsersTokenRepository iUsersTokenRepository, ILogger<TokenService> iLogger, IOptions<JWTSettings> jwtOptions, IOptions<Claims> claims)
    {
        _iUserRepository = iUserRepository;
        _iUsersTokenRepository = iUsersTokenRepository;
        _iLogger = iLogger;
        _jwtSetting = jwtOptions.Value;
        _claims = claims.Value;
    }
    public async Task<TokenResponse> GenerateTokens(UserDTO user)
    {
        _iLogger.LogInformation("Generate refresh token");
        var refreshToken = GenerateSecureRefreshToken(user.Id, _jwtSetting.Secret!);
        //var refreshToken = GenerateRefreshToken();

        _iLogger.LogInformation("Generate access token");
        var accessJwt = GenerateAccessToken(user);

        return await Task.FromResult(new TokenResponse { AccessToken = accessJwt, RefreshToken = refreshToken });
    }
    public async Task<UserResponse?> RefreshAsync(TokenDTO token)
    {
        try
        {
            if (!await ValidateAccessTokenAsync(token.AccessToken))
            {
                _iLogger.LogError("Invalid access token!");
                throw new Exception("Invalid access token!");
            }

            if (!await ValidateRefreshTokenAsync(token.RefreshToken))
            {
                _iLogger.LogError("Invalid refresh token!");
                throw new Exception("Invalid refresh token!");
            }

            var user = (await _iUsersTokenRepository.GetSingleAsync(t => t.RefreshToken.Equals(token.RefreshToken)))!.User;

            if (user is null)
            {
                _iLogger.LogError("In database refresh token is not found!");
                throw new Exception("Refresh token not found in database!");
            }

            var UserDTO = new UserDTO { Id = user.Id, Email = user.Email! };

            _iLogger.LogInformation("Generate tokens");
            var tokens = await GenerateTokens(UserDTO);

            return new UserResponse { User = user.Adapt<UserInfo>(), Token = tokens };
        }
        catch (Exception)
        {
            throw;
        }
    }
    public TokenValidationParameters GetTokenValidationParameters(bool validateLifetime = false) => new()
    {
        ValidateIssuer = true,
        ValidIssuer = _jwtSetting!.Issuer,

        ValidateAudience = true,
        ValidAudience = _jwtSetting.Audience,

        ClockSkew = TimeSpan.Zero,

        ValidateLifetime = validateLifetime,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret!)),
        ValidateIssuerSigningKey = true
    };

    #region Private Methods

    private string GenerateAccessToken(UserDTO user)
    {
        var now = DateTime.UtcNow;
        var expires = now.Add(TimeSpan.FromSeconds(_jwtSetting.ExpireMinutes));

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(_claims.DepartmentId, ""),
            new(_claims.DepartmentName, ""),
            new(_claims.Designation, "")
        };

        var jwt = new JwtSecurityToken(
            _jwtSetting.Issuer,
            _jwtSetting.Audience,
            claims,
            expires: expires,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret!)), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    private string GenerateSecureRefreshToken(Guid userId, string secretKey)
    {
        Span<byte> randomBytes = stackalloc byte[32];
        RandomNumberGenerator.Fill(randomBytes);
        string nonce = Convert.ToBase64String(randomBytes);

        string payload = $"{userId}:{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}:{nonce}";
        string base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload));

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        string signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(base64Payload)));

        return $"{base64Payload}.{signature}";
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
    private async Task<bool> ValidateAccessTokenAsync(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claimsPrincipal = tokenHandler.ValidateToken(accessToken, GetTokenValidationParameters(), out var _);

        var claims = claimsPrincipal.Claims.ToList();

        var userIdClaim = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));
        if (userIdClaim is null)
        {
            return false;
        }

        var userEmailClaim = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));
        if (userEmailClaim is null)
        {
            return false;
        }

        var userExist = await _iUserRepository.Table.AnyAsync(u => u.Id.Equals(new Guid(userIdClaim.Value)) && u.Email!.Equals(userEmailClaim.Value));
        return userExist;
    }
    private async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
    {
        var token = await _iUsersTokenRepository.Table.FirstOrDefaultAsync(t => t.RefreshToken.Equals(refreshToken));
        return token is not null && DateTime.UtcNow < token.DateExpired;
    }

    public (string username, List<string> roles) ValidateJwtToken(string token)
    {
        throw new NotImplementedException();
    }

    #endregion
}
