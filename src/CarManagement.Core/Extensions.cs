using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Entities.Factories;

[assembly: InternalsVisibleTo("CarManagement.Api")]

namespace CarManagement.Core;

internal static class Extensions
{
    internal static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        services.AddScoped<IVehicleFactory, VehicleFactory>();

        return services;
    }
}