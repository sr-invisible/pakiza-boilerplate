using Pakiza.Application.Services.Authentication;

namespace Pakiza.Application.Features.Authentication.Commands;

public class LogoutHandlerCustom(IAuthenticationService iAuthenticationService) : ICommandHandler<LogoutCommand, LogoutResult>
{
    public async Task<LogoutResult> Handle(LogoutCommand cmd, CancellationToken cancellationToken)
    {
        var result = await iAuthenticationService.LogoutAsync(cmd.RefreshToken);
        return new LogoutResult(Message: result);
    }
}
