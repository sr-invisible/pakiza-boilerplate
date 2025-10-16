namespace Pakiza.Application.Helper;

public class SiteSettingHelper
{
    public const string SectionName = "SiteSettings";
    public string BaseUrl { get; init; } = string.Empty;
    public string SspMailPort { get; init; } = string.Empty;
    public string SspMailHost { get; init; } = string.Empty;
    public string SspMailUser { get; init; } = string.Empty;
    public string SspMailPass { get; init; } = string.Empty;
    public string SspMailFrom { get; init; } = string.Empty;
    public string ServerUrl { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string RootDirectoryForPic { get; init; } = string.Empty;
    public string PhotoUrl { get; init; } = string.Empty;
    public string SignUrl { get; init; } = string.Empty;
}
