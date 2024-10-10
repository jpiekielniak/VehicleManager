[assembly: InternalsVisibleTo("CarManagement.Api")]

namespace CarManagement.Infrastructure;

internal static class Extensions
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}