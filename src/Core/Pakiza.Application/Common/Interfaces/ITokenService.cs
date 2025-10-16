using Pakiza.Application.DTOs.Token;
using Pakiza.Application.DTOs.User;

namespace Pakiza.Application.Common.Interfaces;

public interface ITokenService
{
    Task<TokenResponse> GenerateTokens(UserDTO user);
    Task<UserResponse?> RefreshAsync(TokenDTO tokens);
    (string username, List<string> roles) ValidateJwtToken(string token);


}
