using CarManagement.Core.Users.Entities;

namespace CarManagement.Infrastructure.EF;

internal sealed class CarManagementDbContext(DbContextOptions<CarManagementDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<Role> Roles { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}