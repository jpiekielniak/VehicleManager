using CarManagement.Shared.Auth;
using CarManagement.Shared.Auth.Context;
using CarManagement.Shared.Hash;
using CarManagement.Shared.Middlewares.Exceptions;
using CarManagement.Shared.Middlewares.Tokens;

namespace CarManagement.Shared;

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
        app.UseMiddleware<TokenExpirationMiddleware>();

        return app;
    }
}