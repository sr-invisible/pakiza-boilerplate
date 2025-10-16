using Pakiza.Application.DTOs.User;

namespace Pakiza.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<UserResponse> RegistrationAsync(RegisterDTO model);
    Task<UserResponse> LoginAsync(LoginDTO model);
    Task<string> LogoutAsync(string refreshToken);
}
