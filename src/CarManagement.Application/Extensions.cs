using CarManagement.Shared.Validation;

[assembly: InternalsVisibleTo("CarManagement.Api")]

namespace CarManagement.Application;

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