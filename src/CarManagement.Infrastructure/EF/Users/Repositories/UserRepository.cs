using CarManagement.Core.Users.Entities;
using CarManagement.Core.Users.Repositories;

namespace CarManagement.Infrastructure.EF.Users.Repositories;

internal sealed class UserRepository(CarManagementDbContext dbContext) : IUserRepository
{
    private readonly DbSet<User> _users = dbContext.Users;

    public async Task AddAsync(User user, CancellationToken cancellationToken)
        => await _users.AddAsync(user, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);

    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => await _users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<bool> AnyAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        => await _users.AnyAsync(predicate, cancellationToken);

    public async Task<User> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
}