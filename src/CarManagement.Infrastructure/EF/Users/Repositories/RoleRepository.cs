using CarManagement.Core.Users.Entities;
using CarManagement.Core.Users.Repositories;

namespace CarManagement.Infrastructure.EF.Users.Repositories;

internal sealed class RoleRepository(CarManagementDbContext dbContext) : IRoleRepository
{
    private readonly DbSet<Role> _roles = dbContext.Roles;

    public async Task<Role> GetAsync(string role, CancellationToken cancellationToken)
        => await _roles
            .FirstOrDefaultAsync(r => r.Name == role, cancellationToken);
}