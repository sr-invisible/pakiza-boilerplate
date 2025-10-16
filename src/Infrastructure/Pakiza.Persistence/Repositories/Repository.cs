using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pakiza.Application.Common.Interfaces;
using Pakiza.Application.Repositories;

namespace Pakiza.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IAppDbContext _context;

    public Repository(IAppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    #region Command

    public async ValueTask<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Table.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async ValueTask<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await Table.AddRangeAsync(entities, cancellationToken);
        return true;
    }

    public async ValueTask<bool> RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        var entity = await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id), cancellationToken);
        if (entity is null) return false;
        Table.Remove(entity);
        return true;
    }

    public ValueTask<bool> RemoveAsync(T entity, CancellationToken cancellationToken = default)
    {
        Table.Remove(entity);
        return ValueTask.FromResult(true);
    }

    public ValueTask<bool> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        Table.RemoveRange(entities);
        return ValueTask.FromResult(true);
    }

    public ValueTask<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        Table.Update(entity);
        return ValueTask.FromResult(entity);
    }

    #endregion

    #region Query

    public IQueryable<T> GetAll(bool tracking = true, bool includeDeleted = false)
    {
        var query = Table.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        if (!includeDeleted)
            query = query.Where(x => !x.IsDeleted); // Assumes IsDeleted exists in BaseEntity

        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true, bool includeDeleted = false)
    {
        var query = Table.Where(predicate);

        if (!tracking)
            query = query.AsNoTracking();

        if (!includeDeleted)
            query = query.Where(x => !x.IsDeleted);

        return query;
    }

    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var query = tracking ? Table : Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetByIdAsync(string id, bool tracking = true, CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(id, out var guid))
            return null;

        var query = tracking ? Table : Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(e => e.Id == guid, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await Table.AnyAsync(predicate, cancellationToken);
    }

    // Projection: Allows querying with selected fields.
    public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector,
        bool tracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = tracking ? Table : Table.AsNoTracking();
        return await query.Where(predicate).Select(selector).FirstOrDefaultAsync(cancellationToken);
    }

    // Pagination: Supports paging with filtering and sorting.
    public async Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(
        int pageIndex, int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool tracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = Table;

        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        if (!tracking)
            query = query.AsNoTracking();

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    // Eager loading: Supports multiple included related entities.
    public IQueryable<T> GetAllIncluding(bool tracking = true, params Expression<Func<T, object>>[] includes)
    {
        var query = Table.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }

    #endregion

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
