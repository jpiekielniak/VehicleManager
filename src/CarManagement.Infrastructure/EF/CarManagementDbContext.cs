using CarManagement.Core.Users.Entities;
using CarManagement.Core.Vehicles.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarManagement.Infrastructure.EF;

public sealed class CarManagementDbContext(DbContextOptions<CarManagementDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<Role> Roles { get; init; }
    public DbSet<Vehicle> Vehicles { get; init; }
    public DbSet<ServiceBook> ServiceBooks { get; init; }
    public DbSet<Service> Services { get; init; }
    public DbSet<Inspection> Inspections { get; init; }
    public DbSet<Cost> Costs { get; init; }
    public DbSet<Insurance> Insurances { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}