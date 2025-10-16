namespace Pakiza.Application.Services;

public interface IService<T> where T : class
{
    Task<T> AddAsync(T model);
    Task<T> UpdateAsync(T model);
    Task<bool> DeleteByIdAsync(Guid id);
    Task<T> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();
}
