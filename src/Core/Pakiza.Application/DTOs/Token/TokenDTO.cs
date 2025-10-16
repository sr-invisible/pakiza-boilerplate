namespace Pakiza.Application.DTOs.Token;

public record TokenDTO
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
