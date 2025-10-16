namespace Pakiza.Application.Features.DC.DCProject.Commands;

public record UpdateDCProjectCommand(DCProjectDTO DCProjectDTO) : ICommand<UpdateDCProjectResult>;
public record UpdateDCProjectResult(Guid Id);

public class UpdateDCProjectCommandValidator : AbstractValidator<UpdateDCProjectCommand>
{
    public UpdateDCProjectCommandValidator()
    {
        RuleFor(e => e.DCProjectDTO.Id).NotNull().NotEmpty();
        RuleFor(e => e.DCProjectDTO.ProjectName).NotEmpty().MaximumLength(200);
    }
}
