using VehicleManager.Core.Users.Repositories;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Infrastructure.EF;
using VehicleManager.Infrastructure.EF.Initializer;
using VehicleManager.Infrastructure.EF.ServiceBooks.Repositories;
using VehicleManager.Infrastructure.EF.Users.Repositories;
using VehicleManager.Infrastructure.EF.Vehicles.Repositories;
using VehicleManager.Infrastructure.Sieve;

[assembly: InternalsVisibleTo("VehicleManager.Api")]
[assembly: InternalsVisibleTo("VehicleManager.Tests.Integration")]

namespace VehicleManager.Infrastructure;

internal static class Extensions
{
    private const string ConnectionStringName = "VehicleManager";

    internal static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        services
            .AddDbContext<VehicleManagerDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(ConnectionStringName));
            });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IServiceBookRepository, ServiceBookRepository>();
        services.AddScoped<IInspectionRepository, InspectionRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
        services.AddHostedService<MigrationInitializer>();

        return services;
    }
}