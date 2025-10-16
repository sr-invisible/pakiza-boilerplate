namespace Pakiza.Application.Features.DC.SQL.Commands;

public record ExecuteScriptByVersionCommand(SqlScriptDTO SqlScript) : ICommand<ExecuteScriptByVersionResult>;

public record ExecuteScriptByVersionResult(Guid Id);

public class ExecuteScriptByVersionCommandValidator : AbstractValidator<ExecuteScriptByVersionCommand>
{
    public ExecuteScriptByVersionCommandValidator()
    {
        RuleFor(x => x.SqlScript.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.SqlScript.ScriptFile)
            .NotNull().WithMessage("Script file is required.")
            .Must(x => x!.FileName.EndsWith(".sql")).WithMessage("Only .sql files allowed.");

        RuleFor(x => x.SqlScript.Version).NotEmpty().WithMessage("Version is required.")
            .Matches(@"^\d+(\.\d+)*$").WithMessage("Version must be a valid version format (e.g. 1.0, 2.1.3)");

        RuleFor(x => x.SqlScript.Name).NotEmpty().WithMessage("Script name is required.").MaximumLength(255);
    }
}
