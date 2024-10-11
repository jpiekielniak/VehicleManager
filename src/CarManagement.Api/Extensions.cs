using CarManagement.Application;
using CarManagement.Core;
using CarManagement.Infrastructure;
using CarManagement.Shared;

namespace CarManagement.Api;

internal static class Extensions
{
    internal static IServiceCollection LoadLayers(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        services.AddApplication();
        services.AddCore();
        services.AddInfrastructure(configuration);
        services.AddShared(configuration);

        return services;
    }
}