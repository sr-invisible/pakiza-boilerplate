
namespace Pakiza.Application.Repositories;

public interface IDapperRepository<T> where T : BaseEntity
{
    Task<int> AddAsync(T entity);
    Task<int> AddRangeAsync(IEnumerable<T> entities);
    Task<int> RemoveAsync(Guid id);
    Task<int> RemoveAsync(T entity);
    Task<int> RemoveRangeAsync(IEnumerable<T> entities);
    Task<int> UpdateAsync(T entity);

    Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, bool includeDeleted = false);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    Task<TResult?> GetFirstOrDefaultAsync<TResult>(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector);

    Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
        int pageIndex, int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    Task<int> SaveChangesAsync();
}

