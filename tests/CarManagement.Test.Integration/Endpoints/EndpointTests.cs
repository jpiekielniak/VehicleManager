using CarManagement.Core.Users.Entities;
using CarManagement.Infrastructure.EF;
using CarManagement.Shared.Hash;

namespace CarManagement.Test.Integration.Endpoints;

public abstract class EndpointTests : IClassFixture<CarManagementTestFactory>, IAsyncLifetime
{
    private readonly IServiceScope _scope;
    protected readonly HttpClient Client;
    protected readonly CarManagementDbContext DbContext;
    protected readonly IPasswordHasher PasswordHasher;
    protected readonly Role Role = Role.Create("User");

    protected EndpointTests(CarManagementTestFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Client = factory.CreateClient();
        DbContext = _scope.ServiceProvider.GetRequiredService<CarManagementDbContext>();
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