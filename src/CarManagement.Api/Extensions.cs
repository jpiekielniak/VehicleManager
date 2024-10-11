using CarManagement.Application;
using CarManagement.Core;
using CarManagement.Infrastructure;
using CarManagement.Shared;
using CarManagement.Shared.Endpoints;

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

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var endpointDefinitions = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IEndpointDefinition).IsAssignableFrom(t) && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineEndpoints(endpoints);
        }

        return endpoints;
    }
}