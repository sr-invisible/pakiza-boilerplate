using Pakiza.Application.DTOs.User;

namespace Pakiza.Application.Features.Authentication.Commands;

public record RegisterCommand(RegisterDTO Register) : ICommand<RegisterResult>;
public record RegisterResult(UserResponse UserResponse);
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator() 
    {
        RuleFor(x => x.Register.FullName)
                   .NotEmpty()
                   .MaximumLength(100);

        //RuleFor(x => x.Register.LastName)
        //    .NotEmpty()
        //    .MaximumLength(100);

        RuleFor(x => x.Register.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{1,14}$").WithMessage("Invalid phone number format. Use international format, e.g., +88017********.");

        RuleFor(x => x.Register.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Register.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(128);

        RuleFor(x => x.Register.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(256)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
            .WithMessage("Password must contain uppercase, lowercase, digit, and special character.");

        RuleFor(x => x.Register.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Register.Password).WithMessage("The password and confirmation password do not match.");
    }
}
