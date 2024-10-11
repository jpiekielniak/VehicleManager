using CarManagement.Shared.Auth;
using CarManagement.Shared.Middlewares.Exceptions;
using CarManagement.Shared.Middlewares.Tokens;
using CarManagement.Shared.Validation;

namespace CarManagement.Shared;

public static class Extensions
{
    public static IServiceCollection AddShared(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthOptions(configuration);
        services.AddPolicy();
        services.AddValidators(Assembly.GetExecutingAssembly());

        services.AddScoped<IAuthManager, AuthManager>();

        return services;
    }

    public static WebApplication UseMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<TokenExpirationMiddleware>();

        return app;
    }
}