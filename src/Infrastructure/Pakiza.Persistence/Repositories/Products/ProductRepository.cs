

using Pakiza.Application.Repositories;

namespace Pakiza.Persistence.Repositories.Products;

public class ProductRepository : DapperRepository<Product>, IProductRepository
{
    public ProductRepository(IDbConnection dbConnection, AppDbContext context) : base(dbConnection, context)
    {
    }
}
