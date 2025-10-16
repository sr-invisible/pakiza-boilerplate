using Pakiza.Application.Services.Authentication;

namespace Pakiza.Application.Features.Authentication.Commands;

public class LoginHandler(IAuthenticationService iAuthenticationService) : ICommandHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand cmd, CancellationToken cancellationToken)
    {
        var result = await iAuthenticationService.LoginAsync(cmd.LoginDTO);
        return new LoginResult(result);
    }
}
