namespace Pakiza.Application.Features.Products.Commands;

public class UpdateProductHandler(IProductService productService)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand cmd, CancellationToken cancellationToken)
    {
        await productService.UpdateAsync(cmd.Product);
        return new UpdateProductResult(cmd.Product.Id);
    }
}
