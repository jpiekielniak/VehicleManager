using CarManagement.Core.Users.Entities;

namespace CarManagement.Core.Users.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> AnyAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
    Task<User> GetAsync(Guid id, CancellationToken cancellationToken);
}