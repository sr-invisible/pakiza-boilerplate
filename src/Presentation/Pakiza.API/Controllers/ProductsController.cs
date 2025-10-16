
using Mapster;
using Pakiza.Application.Features.Products.Commands;
using Pakiza.Application.Features.Products.Queries;
namespace Pakiza.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProductsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductRequest dto)
    {
        var result = await _mediator.Send(new CreateProductCommand(dto.Adapt<ProductDTO>()));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id,[FromBody] ProductRequest dto)
    {
        var updatedProduct = dto.Adapt<ProductDTO>();
        updatedProduct.Id = id;
        var result = await _mediator.Send(new UpdateProductCommand(updatedProduct));
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetProductsQuery());
        return Ok(result);
    }
}
