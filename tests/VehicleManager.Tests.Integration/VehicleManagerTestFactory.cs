using Microsoft.Extensions.Configuration;
using VehicleManager.Infrastructure.EF;

namespace VehicleManager.Tests.Integration;

public class VehicleManagerTestFactory : WebApplicationFactory<Api.Program>
{
    private const string ConnectionStringSection = "VehicleManagerTest";

    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.Test.json")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<VehicleManagerDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            var connectionString = _configuration.GetConnectionString(ConnectionStringSection);
            var dbName = $"vehicle_manager_test_{Guid.NewGuid()}";
            services.AddDbContext<VehicleManagerDbContext>(options =>
            {
                options.UseNpgsql(connectionString?.Replace("vehicle_manager_test", dbName))
                    .EnableSensitiveDataLogging(false)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        });
    }
}