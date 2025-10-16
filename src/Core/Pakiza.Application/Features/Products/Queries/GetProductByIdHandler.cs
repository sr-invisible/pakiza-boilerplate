namespace Pakiza.Application.Features.Products.Queries;

public class GetProductByIdHandler(IProductService productService)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await productService.GetByIdAsync(query.Id);
        return new GetProductByIdResult(result);
    }
}
