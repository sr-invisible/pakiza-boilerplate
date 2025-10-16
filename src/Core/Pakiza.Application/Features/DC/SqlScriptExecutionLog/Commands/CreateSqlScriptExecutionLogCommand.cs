namespace Pakiza.Application.Features.DC.SqlScriptExecutionLog.Commands;

public record CreateSqlScriptExecutionLogCommand(SqlScriptExecutionLogDTO SqlScriptExecutionLog) : ICommand<CreateSqlScriptExecutionLogResult>;

public record CreateSqlScriptExecutionLogResult(Guid Id);

public class CreateSqlScriptExecutionLogCommandValidator : AbstractValidator<CreateSqlScriptExecutionLogCommand>
{
    public CreateSqlScriptExecutionLogCommandValidator()
    {
        RuleFor(x => x.SqlScriptExecutionLog.ScriptId)
    .NotEmpty().WithMessage("Script ID is required.");

        RuleFor(x => x.SqlScriptExecutionLog.ExecutedAt)
            .NotEmpty().WithMessage("Execution time must be provided.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("ExecutedAt cannot be in the future.");

        RuleFor(x => x.SqlScriptExecutionLog.Status)
            .IsInEnum().WithMessage("Invalid status value.");

        RuleFor(x => x.SqlScriptExecutionLog.Message)
            .NotNull().WithMessage("Message must not be null.")
            .MaximumLength(1000).WithMessage("Message cannot exceed 1000 characters.");
    }
}
