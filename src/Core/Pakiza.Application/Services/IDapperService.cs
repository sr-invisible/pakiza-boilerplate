namespace Pakiza.Application.Services;

public interface IDapperService<T> where T : class
{
    Task<int> AddAsync(T model);
    Task<int> UpdateAsync(T model);
    Task<int> DeleteByIdAsync(Guid id);
    Task<T> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();
}
