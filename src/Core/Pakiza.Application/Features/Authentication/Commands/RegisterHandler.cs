using Pakiza.Application.Services.Authentication;

namespace Pakiza.Application.Features.Authentication.Commands;

public class RegisterHandler(IAuthenticationService iAuthenticationService) : ICommandHandler<RegisterCommand, RegisterResult>
{
    public async Task<RegisterResult> Handle(RegisterCommand cmd, CancellationToken cancellationToken)
    {
        var result = await iAuthenticationService.RegistrationAsync(cmd.Register);
        return new RegisterResult(result);
    }
}

