namespace Pakiza.Application.Features.Products.Commands;

public class CreateProductHandler(IProductService productService)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand cmd, CancellationToken cancellationToken)
    {
        cmd.Product.Id = Guid.NewGuid();
        await productService.AddAsync(cmd.Product);
        return new CreateProductResult(cmd.Product.Id);
    }
}
