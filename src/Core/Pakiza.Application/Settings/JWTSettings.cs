namespace Pakiza.Application.Settings;

public class JWTSettings
{
    public const string SectionName = "JwtSettings";
    public string? Secret { get; init; }
    public string? Authority { get; init; }
    public string? Issuer { get; init; }
    public string? Audience { get; init; }
    public int ExpireMinutes { get; init; }
    public string? PostLogoutRedirectUri { get; init; }

}
