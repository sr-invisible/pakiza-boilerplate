namespace Pakiza.Application.Features.Products.Commands;

public record CreateProductCommand(ProductDTO Product) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Product.Price)
            .GreaterThanOrEqualTo(0);
    }
}

