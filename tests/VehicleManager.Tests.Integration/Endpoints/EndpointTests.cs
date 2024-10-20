using VehicleManager.Core.Users.Entities;
using VehicleManager.Infrastructure.EF;
using VehicleManager.Shared.Hash;

namespace VehicleManager.Tests.Integration.Endpoints;

public abstract class EndpointTests : IClassFixture<VehicleManagerTestFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;
    protected readonly HttpClient Client;
    protected readonly VehicleManagerDbContext DbContext;
    protected readonly IPasswordHasher PasswordHasher;
    protected readonly Role Role = Role.Create("User");

    protected EndpointTests(VehicleManagerTestFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Client = factory.CreateClient();
        DbContext = _scope.ServiceProvider.GetRequiredService<VehicleManagerDbContext>();
        PasswordHasher = _scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    }

    public async Task InitializeAsync()
    {
        await ClearDatabase();

        if (!await DbContext.Roles.AnyAsync(r => r.Name == "User"))
        {
            await DbContext.Roles.AddAsync(Role);
            await DbContext.SaveChangesAsync();
        }
    }

    public Task DisposeAsync() => Task.CompletedTask;

    private async Task ClearDatabase()
    {
        DbContext.Users.RemoveRange(DbContext.Users);
        DbContext.Roles.RemoveRange(DbContext.Roles);
        await DbContext.SaveChangesAsync();
    }
}