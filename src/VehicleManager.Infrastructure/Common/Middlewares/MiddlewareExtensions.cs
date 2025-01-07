using VehicleManager.Infrastructure.Common.Middlewares.Exceptions;

namespace VehicleManager.Infrastructure.Common.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}