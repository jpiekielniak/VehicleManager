using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Infrastructure.EF.Users.Repositories;

internal sealed class UserRepository(VehicleManagerDbContext dbContext) : IUserRepository
{
    private readonly DbSet<User> _users = dbContext.Users;

    public async Task AddAsync(User user, CancellationToken cancellationToken)
        => await _users.AddAsync(user, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);

    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => await _users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<bool> AnyAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        => await _users.AnyAsync(predicate, cancellationToken);

    public async Task<User> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
        => await Task.FromResult(_users.Remove(user));

    public async Task<IQueryable<User>> GetUsersAsync(CancellationToken cancellationToken)
        => await Task.FromResult(_users
            .AsNoTracking()
            .AsQueryable()
            );
}