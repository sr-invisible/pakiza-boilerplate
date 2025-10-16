namespace Pakiza.Persistence.Data.Extensions;

internal static class InitialData
{
    public static List<Role> Roles => new()
        {
            new Role { Name = "Super Admin" },
            new Role { Name = "Admin" },
            new Role { Name = "User" },
            new Role { Name = "Manager" }
        };

    public static List<(User user, string password, string[] roles)> Users => new()
        {
            (
                new User
                {
                    FullName = "Super Admin",
                    UserName = "superadmin@pakiza.com",
                    Email = "superadmin@pakiza.com",

                },
                "Admin@123",
                new[] { "Super Admin" }
            ),
            (
                new User
                { 
                    FullName = "Admin",
                    UserName = "admin@pakiza.com",
                    Email = "admin@pakiza.com",
                },
                "Admin@123",
                new[] { "Admin" }
            ),
            (
                new User
                {
                    FullName = "User",
                    UserName = "user@pakiza.com",
                    Email = "user@pakiza.com",
                },
                "User@123",
                new[] { "User" }
            )
        };
}
