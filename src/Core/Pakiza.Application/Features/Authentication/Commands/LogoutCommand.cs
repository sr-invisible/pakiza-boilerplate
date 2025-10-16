namespace Pakiza.Application.Features.Authentication.Commands;

public record LogoutCommand(string RefreshToken) : ICommand<LogoutResult>;

public record LogoutResult(string Message);

