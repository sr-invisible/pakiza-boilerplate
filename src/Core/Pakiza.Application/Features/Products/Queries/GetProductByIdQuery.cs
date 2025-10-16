namespace Pakiza.Application.Features.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(ProductDTO Product);
