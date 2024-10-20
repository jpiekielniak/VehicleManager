using VehicleManager.Shared.Validation;

[assembly: InternalsVisibleTo("VehicleManager.Api")]

namespace VehicleManager.Application;

internal static class Extensions
{
    internal static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        services
            .AddValidators(Assembly.GetExecutingAssembly());

        return services;
    }
}