
namespace Pakiza.Application.Features.Queries.Products;

public class GetProductsHandler(IProductService productService)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var result = await productService.GetAllAsync();
        return new GetProductsResult(result);
    }
}
