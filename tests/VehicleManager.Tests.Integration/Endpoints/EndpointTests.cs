using VehicleManager.Infrastructure.EF;
using VehicleManager.Shared.Hash;

namespace VehicleManager.Tests.Integration.Endpoints;

public abstract class EndpointTests : IClassFixture<VehicleManagerTestFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;
    protected readonly HttpClient Client;
    protected readonly VehicleManagerDbContext DbContext;
    protected readonly IPasswordHasher PasswordHasher;

    protected EndpointTests(VehicleManagerTestFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Client = factory.CreateClient();
        DbContext = _scope.ServiceProvider.GetRequiredService<VehicleManagerDbContext>();
        PasswordHasher = _scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    }

    public async Task InitializeAsync() => await ClearDatabase();

    public Task DisposeAsync() => Task.CompletedTask;

    private async Task ClearDatabase()
    {
        DbContext.Users.RemoveRange(DbContext.Users);
        await DbContext.SaveChangesAsync();
    }
}