
namespace Pakiza.Persistence.Data.Extensions;
public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }
    
    private static async Task SeedAsync(AppDbContext context)
    {
        await SeedRolesAsync(context);
        await SeedUserAsync(context);
    }
    private static async Task SeedRolesAsync(AppDbContext context)
    {
        if (!await context.Roles.AnyAsync())
        {
            await context.Roles.AddRangeAsync(InitialData.Roles); 
            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedUserAsync(AppDbContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            var passwordHasher = new PasswordHasher<User>();

            foreach (var (user, password, roles) in InitialData.Users)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, password);
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                foreach (var roleName in roles)
                {
                    var role = await context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
                    if (role != null)
                    {
                        var userRole = new UserRole
                        {
                            UserId = user.Id,
                            RoleId = role.Id
                        };

                        await context.Set<UserRole>().AddAsync(userRole);
                    }
                }

                await context.SaveChangesAsync();
            }
        }
    }





}
