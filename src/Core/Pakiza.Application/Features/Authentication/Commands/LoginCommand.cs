using Pakiza.Application.DTOs.User;

namespace Pakiza.Application.Features.Authentication.Commands;

public record LoginCommand(LoginDTO LoginDTO) : ICommand<LoginResult>;

public record LoginResult(UserResponse UserResponse);

public class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
    }
}


