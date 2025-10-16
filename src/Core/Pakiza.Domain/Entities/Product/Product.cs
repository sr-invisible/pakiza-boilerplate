namespace Pakiza.Domain.Entities.Product;

public class Product : BaseEntity
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    [NotMapped]
    public List<ProductDetails> ProductDetails { get; set; } = default!;
}
