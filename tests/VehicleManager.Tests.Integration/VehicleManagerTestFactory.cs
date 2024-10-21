using VehicleManager.Infrastructure.EF;

namespace VehicleManager.Tests.Integration;

public class VehicleManagerTestFactory : WebApplicationFactory<Api.Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("vehicle_manager_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithName($"vehicle_manager_test_{Guid.NewGuid()}")
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
                    .UseNpgsql(_container.GetConnectionString());
            });
        });
    }

    public Task InitializeAsync() => _container.StartAsync();

    public new Task DisposeAsync() => _container.StopAsync();
}