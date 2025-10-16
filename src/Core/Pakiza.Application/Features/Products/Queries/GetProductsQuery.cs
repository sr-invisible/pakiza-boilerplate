namespace Pakiza.Application.Features.Products.Queries;

public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IReadOnlyList<ProductDTO> Product);
