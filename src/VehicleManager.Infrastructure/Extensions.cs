using VehicleManager.Core.Common.Security;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Infrastructure.Common.Authentication;
using VehicleManager.Infrastructure.Common.BackgroundJobs;
using VehicleManager.Infrastructure.Common.Emails;
using VehicleManager.Infrastructure.Common.Security;
using VehicleManager.Infrastructure.Common.Sieve;
using VehicleManager.Infrastructure.EF;
using VehicleManager.Infrastructure.EF.Initializer;
using VehicleManager.Infrastructure.EF.ServiceBooks.Repositories;
using VehicleManager.Infrastructure.EF.Users.Repositories;
using VehicleManager.Infrastructure.EF.Vehicles.Repositories;

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
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
        services.AddHostedService<MigrationInitializer>();

        services.AddEmail(configuration);
        services.AddAuth(configuration);
        services.AddBackgroundJobs(configuration);

        return services;
    }
}