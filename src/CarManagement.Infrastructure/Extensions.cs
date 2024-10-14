using CarManagement.Core.Users.Repositories;
using CarManagement.Infrastructure.EF;
using CarManagement.Infrastructure.EF.Initializer;
using CarManagement.Infrastructure.EF.Users.Repositories;

[assembly: InternalsVisibleTo("CarManagement.Api")]
[assembly: InternalsVisibleTo("CarManagement.Test.Integration")]

namespace CarManagement.Infrastructure;

internal static class Extensions
{
    private const string ConnectionStringName = "CarManagement";

    internal static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        services
            .AddDbContext<CarManagementDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(ConnectionStringName));
            });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddHostedService<MigrationInitializer>();

        return services;
    }
}