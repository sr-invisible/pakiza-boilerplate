
using Pakiza.Application.Common.Interfaces;
using Pakiza.Infrastructure.Services.Tokens;

namespace Pakiza.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.AddJWT(configuration);

        #region Background Services

        // Add Quartz services
        services.AddQuartz(q =>
        {
            // Register job
            var jobKey = new JobKey("ExpiredTokenCleanupJob");
            q.AddJob<ExpiredTokenCleanupJob>(opts => opts.WithIdentity(jobKey));

            // Trigger: every 3 minutes
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("ExpiredTokenCleanupTrigger")
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMinutes(3))
                    .RepeatForever()));
        });

        // Register Quartz hosted service
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        #endregion

        return services;
    }

    public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration configuration)
    {
        #region Jwt Settings

        var jwtSettings = new JWTSettings();
        configuration.Bind(JWTSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = jwtSettings.Authority;
                options.Audience = jwtSettings.Audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret!)),
                    ClockSkew = TimeSpan.FromHours(jwtSettings.ExpireMinutes),
                };
                options.Configuration = new OpenIdConnectConfiguration();
            });

        services.AddAuthorization();

        #endregion

        #region services
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        #endregion

        return services;
    }

}
