using VehicleManager.Application.Common.Behaviors.Validation;
using VehicleManager.Application.Integrations.BlobStorage;

[assembly: InternalsVisibleTo("VehicleManager.Api")]

namespace VehicleManager.Application;

internal static class Extensions
{
    internal static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        services.AddValidators(Assembly.GetExecutingAssembly());

        services.AddBlobStorage(configuration);

        return services;
    }
}