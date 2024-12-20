using VehicleManager.Shared.Auth;
using VehicleManager.Shared.Auth.Context;
using VehicleManager.Shared.Hash;
using VehicleManager.Shared.Middlewares.Exceptions;
using VehicleManager.Shared.Middlewares.Tokens;

namespace VehicleManager.Shared;

public static class Extensions
{
    public static IServiceCollection AddShared(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthOptions(configuration);

        services.AddScoped<IAuthManager, AuthManager>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IContext, Context>();

        return services;
    }

    public static WebApplication UseMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
   //     app.UseMiddleware<TokenExpirationMiddleware>();

        return app;
    }
}