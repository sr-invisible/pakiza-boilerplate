
using Pakiza.Domain.Entities.DC;

namespace Pakiza.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<T> Set<T>() where T : class;

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserToken> UsersToken { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SqlScript> SqlScripts { get; set; }
    public DbSet<SqlScriptExecutionLog> SqlScriptExecutionLogs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
