using VehicleManager.Core.Users.Entities;

namespace VehicleManager.Core.Users.Repositories;

public interface IRoleRepository
{
    Task<Role> GetAsync(string role, CancellationToken cancellationToken);
    Task AddAsync(Role role, CancellationToken cancellationToken);
}