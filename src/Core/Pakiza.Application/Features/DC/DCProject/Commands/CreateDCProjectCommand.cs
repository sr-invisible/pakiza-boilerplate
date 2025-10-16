namespace Pakiza.Application.Features.DC.DCProject.Commands;

public record CreateDCProjectCommand(DCProjectDTO DCProject) : ICommand<CreateDCProjectResult>;
public record CreateDCProjectResult(Guid id);

public class CreateDCProjectCommandValidator : AbstractValidator<CreateDCProjectCommand>
{
    public CreateDCProjectCommandValidator()
    {
        RuleFor(e => e.DCProject.ProjectName).NotEmpty().MaximumLength(200);
    }
}
