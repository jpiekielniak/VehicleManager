using CarManagement.Core.Users.Entities;

namespace CarManagement.Core.Users.Repositories;

public interface IRoleRepository
{
    Task<Role> GetAsync(string role, CancellationToken cancellationToken);
    Task AddAsync(Role role, CancellationToken cancellationToken);
}