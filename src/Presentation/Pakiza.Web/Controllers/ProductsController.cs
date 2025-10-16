using Pakiza.Application.Features.Products.Commands;
using Pakiza.Application.Features.Products.Queries;

namespace Pakiza.Web.Controllers;

[Authorize]
public class ProductsController : BaseMvcController
{
    public ProductsController(IMediator mediator) : base(mediator) { }

    public async Task<IActionResult> Index()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return View(products);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
            return NotFound();

        return View(product);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductRequest dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _mediator.Send(new CreateProductCommand(dto.Adapt<ProductDTO>()));
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
            return NotFound();

        var dto = product.Adapt<ProductRequest>();
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProductRequest dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var productDto = dto.Adapt<ProductDTO>();
        productDto.Id = id;

        await _mediator.Send(new UpdateProductCommand(productDto));
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return RedirectToAction(nameof(Index));
    }
}
