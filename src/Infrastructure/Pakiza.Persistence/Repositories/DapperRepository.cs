
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Pakiza.Application.Repositories
{
    public class DapperRepository<T> : IDapperRepository<T> where T : BaseEntity
    {
        private readonly IDbConnection _dbConnection;
        private readonly AppDbContext _context;

        public DapperRepository(IDbConnection dbConnection, AppDbContext context)
        {
            _dbConnection = dbConnection;
            _context = context;
        }

        private string GetTableNameForEntity()
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            return entityType.GetTableName();
        }

        // Implementation of AddAsync
        public async Task<int> AddAsync(T entity)
        {
            var tableName = GetTableNameForEntity();
            var columns = string.Join(",", typeof(T).GetProperties().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).Select(p => p.Name));
            var parameters = string.Join(",", typeof(T).GetProperties().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).Select(p => "@" + p.Name));
            var query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";
            return await _dbConnection.ExecuteAsync(query, entity);
        }

        // Implementation of AddRangeAsync
        public async Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            var tableName = GetTableNameForEntity();
            var columns = string.Join(",", typeof(T).GetProperties().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).Select(p => p.Name));
            var parameters = string.Join(",", typeof(T).GetProperties().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).Select(p => "@" + p.Name));
            var query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";
            return await _dbConnection.ExecuteAsync(query, entities);
        }

        // Implementation of RemoveAsync (by ID)
        public async Task<int> RemoveAsync(Guid id)
        {
            var tableName = GetTableNameForEntity();
            var query = $"DELETE FROM {tableName} WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        // Implementation of RemoveAsync (by entity)
        public async Task<int> RemoveAsync(T entity)
        {
            var tableName = GetTableNameForEntity();
            var query = $"DELETE FROM {tableName} WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(query, new { Id = entity.Id });
        }

        // Implementation of RemoveRangeAsync
        public async Task<int> RemoveRangeAsync(IEnumerable<T> entities)
        {
            var tableName = GetTableNameForEntity();
            var query = $"DELETE FROM {tableName} WHERE Id IN @Ids";
            return await _dbConnection.ExecuteAsync(query, new { Ids = entities.Select(e => e.Id) });
        }

        // Implementation of UpdateAsync
        public async Task<int> UpdateAsync(T entity)
        {
            var tableName = GetTableNameForEntity();
            var setClause = string.Join(",", typeof(T).GetProperties().Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null).Where(p => p.Name != "Id").Select(p => $"{p.Name} = @{p.Name}"));
            var query = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(query, entity);
        }

        // Implementation of GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false)
        {
            var tableName = GetTableNameForEntity();
            var query = $"SELECT * FROM {tableName}" + (includeDeleted ? "" : " WHERE IsDeleted = 0");
            return await _dbConnection.QueryAsync<T>(query);
        }

        // Implementation of GetWhereAsync
        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, bool includeDeleted = false)
        {
            var tableName = GetTableNameForEntity();
            var query = $"SELECT * FROM {tableName} WHERE {predicate.Body}"; // Note: You would need to handle predicate parsing here
            return await _dbConnection.QueryAsync<T>(query);
        }

        // Implementation of GetSingleAsync
        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            var tableName = GetTableNameForEntity();
            var query = $"SELECT * FROM {tableName} WHERE {predicate.Body}"; // Handle predicate parsing here as well
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(query);
        }

        // Implementation of GetByIdAsync
        public async Task<T?> GetByIdAsync(Guid id)
        {
            var tableName = GetTableNameForEntity();
            var query = $"SELECT * FROM {tableName} WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
        }

        // Implementation of ExistsAsync
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            var tableName = GetTableNameForEntity();
            var query = $"SELECT COUNT(1) FROM {tableName} WHERE {predicate.Body}"; // Handle predicate parsing
            var result = await _dbConnection.ExecuteScalarAsync<int>(query);
            return result > 0;
        }

        // Implementation of GetFirstOrDefaultAsync with projection
        public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector)
        {
            var tableName = GetTableNameForEntity();
            var query = $"SELECT * FROM {tableName} WHERE {predicate.Body}"; // Handle predicate parsing
            return await _dbConnection.QueryFirstOrDefaultAsync<TResult>(query);
        }

        // Implementation of GetPagedAsync
        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            int pageIndex, int pageSize,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var tableName = GetTableNameForEntity();
            var query = $"SELECT * FROM {tableName}"; // Handle pagination and filtering here
            var totalCountQuery = $"SELECT COUNT(1) FROM {tableName}";
            var totalCount = await _dbConnection.ExecuteScalarAsync<int>(totalCountQuery);

            var items = await _dbConnection.QueryAsync<T>(query); // Add pagination logic
            return (items, totalCount);
        }

        // Implementation of SaveChangesAsync (optional)
        public async Task<int> SaveChangesAsync()
        {
            // Dapper doesn't have change tracking like EF Core, so this is left empty or used for custom logic
            return await Task.FromResult(0);
        }
    }
}
