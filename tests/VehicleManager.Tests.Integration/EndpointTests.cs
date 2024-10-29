using System.Net.Http.Headers;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Infrastructure.EF;
using VehicleManager.Shared.Auth;
using VehicleManager.Shared.Hash;

namespace VehicleManager.Tests.Integration;

[Collection("IntegrationTests")]
public abstract class EndpointTests : IClassFixture<VehicleManagerTestFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly IAuthManager _authManager;
    protected readonly HttpClient Client;
    protected readonly VehicleManagerDbContext DbContext;
    protected readonly IPasswordHasher PasswordHasher;


    protected EndpointTests()
    {
        var factory = new VehicleManagerTestFactory();
        _scope = factory.Services.CreateScope();
        _authManager = _scope.ServiceProvider.GetRequiredService<IAuthManager>();
        Client = factory.CreateClient();
        DbContext = _scope.ServiceProvider.GetRequiredService<VehicleManagerDbContext>();
        PasswordHasher = _scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    }

    public async Task InitializeAsync() => await DbContext.Database.EnsureCreatedAsync();

    public async Task DisposeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.DisposeAsync();
    }

    protected void Authorize(Guid userId, string role)
    {
        var jwt = _authManager.GenerateToken(userId, role);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);
    }

    protected async Task SeedDataAsync(User? user = default, Vehicle? vehicle = default,
        List<Vehicle>? vehicles = default)
    {
        if (user is not null)
        {
            await DbContext.Users.AddAsync(user);
        }

        if (vehicle is not null)
        {
            await DbContext.Vehicles.AddAsync(vehicle);
        }

        if (vehicles is not null)
        {
            await DbContext.Vehicles.AddRangeAsync(vehicles);
        }

        await DbContext.SaveChangesAsync();
    }
}