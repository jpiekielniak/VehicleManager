using CarManagement.Infrastructure.EF;

namespace CarManagement.Test.Integration;

public class CarManagementTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("car_management_test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithName($"car_management_test_{Guid.NewGuid()}")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<CarManagementDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<CarManagementDbContext>(options =>
            {
                options
                    .UseNpgsql(_container.GetConnectionString());
            });
        });
    }

    public Task InitializeAsync() => _container.StartAsync();

    public new Task DisposeAsync() => _container.StopAsync();
}