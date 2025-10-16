using Pakiza.Application.DTOs.User;

namespace Pakiza.Application.Response;

public class AuthenticationResponse
{
    public UserDTO User { get; set; } = default!;
    public TokenResponse Token { get; set; } = default!;

}
