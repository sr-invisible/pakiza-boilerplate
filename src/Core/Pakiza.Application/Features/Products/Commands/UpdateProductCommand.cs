namespace Pakiza.Application.Features.Products.Commands;

public record UpdateProductCommand(ProductDTO Product) : ICommand<UpdateProductResult>;

public record UpdateProductResult(Guid Id);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Id)
            .NotEmpty();
        RuleFor(x => x.Product.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Product.Price)
            .GreaterThanOrEqualTo(0);
    }
}

