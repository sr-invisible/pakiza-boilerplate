
using Pakiza.Application.Constant;
using Pakiza.Application.Services.Authentication;
using Pakiza.Application.Services.User;
using Microsoft.AspNetCore.Authentication;
using Pakiza.Application.Helper.LocalStorageHelper;

namespace Pakiza.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region StaticMessages

        var staticMessages = new StaticMessages();
        services.AddSingleton(Options.Create(staticMessages));
        #endregion

        #region Service
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IFileStorageHelperService, FileStorageHelperService>();
        #endregion

        #region MediatR

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddFeatureManagement();
        //services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        #endregion



        return services;
    }
}
