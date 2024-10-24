using Microsoft.Extensions.Configuration;
using VehicleManager.Infrastructure.EF;

namespace VehicleManager.Tests.Integration;

public class VehicleManagerTestFactory : WebApplicationFactory<Api.Program>
{
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

            services.AddDbContext<VehicleManagerDbContext>(options =>
            {
                options
                    .UseNpgsql(_configuration.GetConnectionString("VehicleManagerTest"));
            });
        });
    }
}