using CarManagement.Core.Users.Entities;
using CarManagement.Core.Vehicles.Entities;

namespace CarManagement.Infrastructure.EF;

public sealed class CarManagementDbContext(DbContextOptions<CarManagementDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<Role> Roles { get; init; }
    public DbSet<Vehicle> Vehicles { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}