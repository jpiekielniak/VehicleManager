[assembly: InternalsVisibleTo("CarManagement.Api")]

namespace CarManagement.Core;

internal static class Extensions
{
    internal static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        return services;
    }
}