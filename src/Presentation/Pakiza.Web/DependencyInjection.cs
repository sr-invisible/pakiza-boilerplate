
namespace Pakiza.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddMvcServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Exception handling
        services.AddExceptionHandler<CustomExceptionHandler>();

        // Health checks
        services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Database")!);

        // API versioning (optional for hybrid apps with API + MVC)
        services
            .AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new QueryStringApiVersionReader("x-api-version"),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version")
                );
            })
            .AddApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

        // CORS policy (adjust as needed for front-end or external access)
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:3001") // React/Vue frontend
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
        });

        return services;
    }

    public static WebApplication UseMvcServices(this WebApplication app)
    {
        // Global error handler
        app.UseExceptionHandler("/Home/Error"); // Fallback to MVC error page

        // Health check endpoint
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
