using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.EF;

public sealed class VehicleManagerDbContext(DbContextOptions<VehicleManagerDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<Vehicle> Vehicles { get; init; }
    public DbSet<ServiceBook> ServiceBooks { get; init; }
    public DbSet<Service> Services { get; init; }
    public DbSet<Inspection> Inspections { get; init; }
    public DbSet<Cost> Costs { get; init; }
    public DbSet<Insurance> Insurances { get; init; }
    public DbSet<Image> Images { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}