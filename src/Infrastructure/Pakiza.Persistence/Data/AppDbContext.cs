
namespace Pakiza.Persistence.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserToken> UsersToken { get; set; }
    public DbSet<Product> Products { get; set; }

    #region DC
    public DbSet<DCCompanyInfo> DCCompanyInfo { get; set; }
    public DbSet<DCProject> DCProjects { get; set; }
    public DbSet<DCModule> DCModules { get; set; }
    public DbSet<DCCompanyProject> DCCompanyProjects { get; set; }
    public DbSet<DCCompanyProjectModules> DCCompanyProjectModules { get; set; }
    public DbSet<SqlScript> SqlScripts { get; set; }
    public DbSet<SqlScriptExecutionLog> SqlScriptExecutionLogs { get; set; }
    #endregion
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<UserToken>().ToTable("UsersToken")
            .HasOne(ut => ut.User)
            .WithMany(u => u.Tokens)
            .HasForeignKey(ut => ut.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany()
        .HasForeignKey(ur => ur.RoleId);

        builder.Entity<Product>()
        .Property(p => p.Price)
        .HasColumnType("decimal(18,4)");
        }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.DateUpdated = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }


}

