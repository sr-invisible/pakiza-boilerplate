namespace Pakiza.Application.Features.DC.SQL.Commands;

public record DeleteSqlScriptCommand(Guid Id) : ICommand<DeleteSqlScriptResult>;

public record DeleteSqlScriptResult(bool IsDeleted);

public class DeleteSqlScriptCommandValidator : AbstractValidator<DeleteSqlScriptCommand>
{
    public DeleteSqlScriptCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}

