namespace Pakiza.Application.Services.User;

public interface IUsersTokenService
{
    Task SaveRefreshTokenAsync(Guid userId, string refreshToken);
    Task<string> RemoveRefreshTokenAsync(string refreshToken);
    Task RemoveExpiredTokensAsync();
 
}
