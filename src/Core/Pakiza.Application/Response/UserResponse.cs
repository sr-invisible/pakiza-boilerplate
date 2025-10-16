namespace Pakiza.Application.Response;

public class UserResponse
{
    public UserInfo User { get; set; } = default!;
    public TokenResponse Token { get; set; } = default!;

}
