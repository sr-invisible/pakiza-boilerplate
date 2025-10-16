
namespace Pakiza.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Connection String 

        string dbTypeName = configuration["Database:TypeName"] ?? throw new InvalidOperationException("Database type not specified in configuration.");
        string dbConnectionType = configuration["Database:ConnectionType"] ?? throw new InvalidOperationException("Database connection type not specified in configuration.");
        var connection = dbTypeName == "MSSQL" ? dbConnectionType == "LIVE" ? "SQLLiveConnectionString" : "SQLLocalConnectionString" : "";
        
        #endregion

        #region Database >> Authentication >> Authorization

        // Add Database Context
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString(connection));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IDbConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(connection);
            return new SqlConnection(connectionString);
        });

        #endregion

        // Add services to the container.

        services.AddScoped<IAppDbContext, AppDbContext>();
        services.AddScoped<AppDbContextSQLDapper>();


        #region Services

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUsersTokenService, UsersTokenService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IDCCompanyInfoService, DCCompanyInfoService>();
        services.AddScoped<IDCProjectService, DCProjectService>();
        services.AddScoped<ISqlScriptService, SqlScriptService>();
        services.AddScoped<ISqlScriptExecutionLogService, SqlScriptExecutionLogService>();

        #endregion

        #region Repositories

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUsersTokenRepository, UsersTokenRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDCCompanyInfoRepository, DCCompanyInfoRepository>();
        services.AddScoped<IDCProjectRepository, DCProjectRepository>();
        services.AddScoped<ISqlScriptRepository, SqlScriptRepository>();
        services.AddScoped<ISqlScriptExecutionLogRepository, SqlScriptExecutionLogRepository>();

        #endregion


        return services;
    }
}
