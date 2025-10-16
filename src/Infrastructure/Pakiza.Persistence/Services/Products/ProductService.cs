
using Mapster;
using Pakiza.Application.DTOs.Products;
using Pakiza.Application.Services.Products;

namespace Pakiza.Persistence.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _iProductRepository;
    public ProductService(IProductRepository iProductRepository)
    {
        _iProductRepository = iProductRepository;
    }
    public async Task<int> AddAsync(ProductDTO entity)
    {
        return await _iProductRepository.AddAsync(entity.Adapt<Product>());
    }

    public async Task<int> DeleteByIdAsync(Guid id)
    {
        return await _iProductRepository.RemoveAsync(id);
    }

    public async Task<IReadOnlyList<ProductDTO>> GetAllAsync()
    {
        return (await _iProductRepository.GetAllAsync(false)).Adapt<IReadOnlyList<ProductDTO>>();
    }
    public async Task<ProductDTO> GetByIdAsync(Guid id)
    {
        return (await _iProductRepository.GetByIdAsync(id)).Adapt<ProductDTO>();
    }
    public async Task<int> UpdateAsync(ProductDTO entity)
    {
        var dbData = await _iProductRepository.GetByIdAsync(entity.Id);
        if (dbData == null) 
            throw new ArgumentNullException(nameof(entity));
        dbData.Name = entity.Name;
        dbData.Price = entity.Price;
        return await _iProductRepository.UpdateAsync(dbData);
    }
}
