using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Pakiza.Application.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    DbSet<T> Table { get; }

    #region Command

    ValueTask<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    ValueTask<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    ValueTask<bool> RemoveAsync(string id, CancellationToken cancellationToken = default);
    ValueTask<bool> RemoveAsync(T entity, CancellationToken cancellationToken = default);
    ValueTask<bool> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    ValueTask<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    #endregion

    #region Query

    IQueryable<T> GetAll(bool tracking = true, bool includeDeleted = false);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true, bool includeDeleted = false);

    Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = true, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(string id, bool tracking = true, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    // Projection support
    Task<TResult?> GetFirstOrDefaultAsync<TResult>(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector,
        bool tracking = true,
        CancellationToken cancellationToken = default);

    // Pagination support
    Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(
        int pageIndex, int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool tracking = true,
        CancellationToken cancellationToken = default);

    // Eager loading support
    IQueryable<T> GetAllIncluding(bool tracking = true, params Expression<Func<T, object>>[] includes);

    #endregion

    Task<int> SaveChangesAsync();
}