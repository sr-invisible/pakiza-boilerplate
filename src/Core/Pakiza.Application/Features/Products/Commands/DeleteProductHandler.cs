namespace Pakiza.Application.Features.Products.Commands;

public class DeleteProductHandler(IProductService productService)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand cmd, CancellationToken cancellationToken)
    {
        await productService.DeleteByIdAsync(cmd.Id);
        return new DeleteProductResult(true);
    }
}
