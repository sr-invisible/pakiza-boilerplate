namespace Pakiza.Application.Features.DC.SQL.Commands;

public record UpdateSqlScriptCommand(ProductDTO Product) : ICommand<UpdateSqlScriptResult>;

public record UpdateSqlScriptResult(Guid Id);

public class UpdateSqlScriptCommandValidator : AbstractValidator<UpdateSqlScriptCommand>
{
    public UpdateSqlScriptCommandValidator()
    {
        RuleFor(x => x.Product.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Product.Price)
            .GreaterThanOrEqualTo(0);
    }
}
