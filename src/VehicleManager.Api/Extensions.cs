using VehicleManager.Application;
using VehicleManager.Core;
using VehicleManager.Infrastructure;
using VehicleManager.Shared;
using VehicleManager.Shared.Endpoints;

namespace VehicleManager.Api;

internal static class Extensions
{
    internal static IServiceCollection LoadLayers(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        services.AddApplication(configuration);
        services.AddCore();
        services.AddInfrastructure(configuration);
        services.AddShared(configuration);

        return services;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var endpointDefinitions = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IEndpointDefinition).IsAssignableFrom(t) && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineEndpoint(endpoint);
        }

        return endpoint;
    }
}