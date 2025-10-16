using Pakiza.Application.Common.Interfaces;
using Pakiza.Application.Repositories.Users;
using Pakiza.Application.Services.User;

namespace Pakiza.Persistence.Services.Users;

public class UsersTokenService : IUsersTokenService
{
    private readonly ILogger<UsersTokenService> logger;
    private readonly JWTSettings _jwtSetting;
    private readonly IUsersTokenRepository _iUsersTokenRepository;
    private readonly ITokenService _iTokenService;

    public UsersTokenService(IUsersTokenRepository iUsersTokenRepository, ITokenService iTokenService, ILogger<UsersTokenService> logger, IOptions<JWTSettings> jwtOptions)
    {
        this.logger = logger;
        _jwtSetting = jwtOptions.Value;
        _iUsersTokenRepository = iUsersTokenRepository;
        _iTokenService = iTokenService;
    }
    #region Public Methods
    public async Task SaveRefreshTokenAsync(Guid userId, string refreshToken)
    {
        try
        {
            var userToken = await _iUsersTokenRepository.GetSingleAsync(t => t.UserId.Equals(userId), false);

            if (userToken is not null)
            {
                var now = DateTime.UtcNow;

                userToken.RefreshToken = refreshToken;
                userToken.DateCreated = now;
                userToken.DateExpired = now.AddMinutes(userToken.LifeTime);
            }
            else
            {
                var tokenModel = new UserToken { UserId = userId, RefreshToken = refreshToken };
                var token = await _iUsersTokenRepository.AddAsync(tokenModel);
                await _iUsersTokenRepository.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<string> RemoveRefreshTokenAsync(string refreshToken)
    {
        try
        {
            var token = await _iUsersTokenRepository.GetSingleAsync(t => t.RefreshToken.Equals(refreshToken));
            if (token == null)
            {
                logger.LogError("In database refresh token is not found!");
                throw new Exception("In database refresh token is not found!");
            }
            _iUsersTokenRepository.Table.Remove(token);
            await _iUsersTokenRepository.SaveChangesAsync();
            return refreshToken;
        }
        catch (Exception) 
        {
            throw;
        }
    }

    public async Task RemoveExpiredTokensAsync()
    {
        try
        {
            var tokens = await _iUsersTokenRepository.GetWhere(t => DateTime.UtcNow >= t.DateExpired).ToListAsync();
            if (tokens.Any())
            {
                await _iUsersTokenRepository.RemoveRangeAsync(tokens);
                await _iUsersTokenRepository.SaveChangesAsync();
            }
        }
        catch (Exception) 
        {
            throw;
        }

    }

    #endregion
}

