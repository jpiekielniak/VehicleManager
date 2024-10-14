using CarManagement.Core.Users.Entities;
using CarManagement.Core.Users.Repositories;

namespace CarManagement.Infrastructure.EF.Users.Repositories;

internal sealed class RoleRepository(CarManagementDbContext dbContext) : IRoleRepository
{
    private readonly DbSet<Role> _roles = dbContext.Roles;

    public async Task<Role> GetAsync(string role, CancellationToken cancellationToken)
        => await _roles
            .FirstOrDefaultAsync(r => r.Name == role, cancellationToken);

    public async Task AddAsync(Role role, CancellationToken cancellationToken)
    {
        await _roles.AddAsync(role, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }

    private async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);
}