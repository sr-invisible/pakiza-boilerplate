namespace Pakiza.Application.DTOs.Products;

public class ProductDTO : BaseDTO
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}
