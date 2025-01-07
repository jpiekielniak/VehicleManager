using VehicleManager.Application.Common.Interfaces.Auth;
using VehicleManager.Application.Common.Interfaces.Context;
using VehicleManager.Infrastructure.Common.Authentication.Options;
using VehicleManager.Infrastructure.Common.Authentication.Services;

namespace VehicleManager.Infrastructure.Common.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthOptions(configuration);

        services.AddScoped<IAuthManager, AuthManager>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IContext, Context.Context>();

        return services;
    }
}