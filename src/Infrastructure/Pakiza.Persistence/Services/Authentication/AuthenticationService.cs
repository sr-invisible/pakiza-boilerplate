using Pakiza.Application.DTOs.User;

namespace Pakiza.Persistence.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _iUserRepository;
    private readonly IRoleRepository _iRoleRepository;
    private readonly IUserRoleRepository _iUserRoleRepository;
    private readonly IUsersTokenService _iUsersTokenService;
    private readonly ITokenService _iTokenService;
    private readonly IPasswordHasher _iPasswordHasher;

    public AuthenticationService(IUserRepository iUserRepository,IRoleRepository iRoleRepository,IUserRoleRepository iUserRoleRepository,
        IUsersTokenService iUsersTokenService, ITokenService iTokenService, IPasswordHasher iPasswordHasher)
    {
        _iUserRepository = iUserRepository;
        _iRoleRepository = iRoleRepository;
        _iUserRoleRepository = iUserRoleRepository;
        _iUsersTokenService = iUsersTokenService;
        _iTokenService = iTokenService;
        _iPasswordHasher = iPasswordHasher;
    }

    public async Task<UserResponse> RegistrationAsync(RegisterDTO model)
    {
        var existingUser = await _iUserRepository.GetSingleAsync(x=> x.Email.Equals(model.Email));
        if (existingUser != null)
            throw new Exception("User already exists");
        var user = model.Adapt<User>();
        user.PasswordHash = _iPasswordHasher.HashPassword(model.Password);

        var result = await _iUserRepository.AddAsync(user);
        if (result == null)
            throw new Exception("User creation failed");

        foreach (var role in model.Roles)
        {
            var dbRole = await _iRoleRepository.GetSingleAsync(x => x.Name.Equals(role));
            if (dbRole == null || !dbRole!.Name.Equals(role))
            {
                dbRole = await _iRoleRepository.AddAsync(new Role { Name = role });
                if (dbRole == null)
                    throw new Exception($"Failed to create role: {role}");
            }

            await _iUserRoleRepository.AddAsync(new UserRole { UserId = result.Id, RoleId = dbRole.Id });
        }
        await _iUserRepository.SaveChangesAsync();
        var userDTO = new UserDTO { Id = result.Id, Email = result.Email };
        var tokens = await _iTokenService.GenerateTokens(userDTO);

        await _iUsersTokenService.SaveRefreshTokenAsync(userDTO.Id, tokens.RefreshToken);
        await _iUserRoleRepository.SaveChangesAsync();
        return new UserResponse { User = user.Adapt<UserInfo>(), Token = tokens };
    }

    public async Task<UserResponse> LoginAsync(LoginDTO model)
    {
        var user = await _iUserRepository.GetSingleAsync(x => x.Email.Equals(model.UserName));
        if (user == null)
            user = await _iUserRepository.GetSingleAsync(x => x.UserName.Equals(model.UserName));
        if (user == null)
            throw new Exception("User not found");

        if (!_iPasswordHasher.VerifyPassword(user.PasswordHash, model.Password))
        {
            throw new Exception("Invalid credentials.");
        }


        var userDTO = new UserDTO { Id = user.Id, Email = user.Email! };
        var tokens = await _iTokenService.GenerateTokens(userDTO);

        await _iUsersTokenService.SaveRefreshTokenAsync(userDTO.Id, tokens.RefreshToken);
        await _iUserRepository.SaveChangesAsync();
        return new UserResponse { User = user.Adapt<UserInfo>(), Token = tokens };
    }

    public async Task<string> LogoutAsync(string refreshToken)
    {
        var removed = await _iUsersTokenService.RemoveRefreshTokenAsync(refreshToken);
        return removed;
    }
}

