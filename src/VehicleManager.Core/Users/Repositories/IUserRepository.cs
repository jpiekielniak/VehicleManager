using VehicleManager.Core.Users.Entities;

namespace VehicleManager.Core.Users.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
    Task<User> GetAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAsync(User user, CancellationToken cancellationToken);
    Task<IQueryable<User>> GetUsersAsync(CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
}