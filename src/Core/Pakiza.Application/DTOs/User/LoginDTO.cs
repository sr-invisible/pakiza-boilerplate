namespace Pakiza.Application.DTOs.User;

public record LoginDTO
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool RememberMe { get; set; } = false;
}
