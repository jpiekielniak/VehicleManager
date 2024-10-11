using CarManagement.Core.Users.Entities;

namespace CarManagement.Infrastructure.EF;

internal sealed class CarManagementDbContext(DbContextOptions<CarManagementDbContext> options)
    : DbContext(options)
{
    private const string Schema = "car_management";
    public DbSet<User> Users { get; init; }
    public DbSet<Role> Roles { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schema);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}