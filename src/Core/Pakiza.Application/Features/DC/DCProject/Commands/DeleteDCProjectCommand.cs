namespace Pakiza.Application.Features.DC.DCProject.Commands;

public record DeleteDCProjectCommand(Guid Id) : ICommand<DeleteDCProjectResult>;
public record DeleteDCProjectResult(bool isDeleted);


public class DeleteDCProjectCommandValidator : AbstractValidator<DeleteDCProjectCommand>
{
    public DeleteDCProjectCommandValidator()
    {
        RuleFor(e => e.Id).NotNull().NotEmpty();
    }
}
