using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Repositories;

namespace CarManagement.Infrastructure.EF.Vehicles.Repositories;

internal sealed class VehicleRepository(CarManagementDbContext dbContext) : IVehicleRepository
{
    private readonly DbSet<Vehicle> _vehicles = dbContext.Vehicles;

    public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
        => await _vehicles.AddAsync(vehicle, cancellationToken);

    public async Task<bool> ExistsAsync(string vin, Guid userId, CancellationToken cancellationToken)
        => await _vehicles.AnyAsync(v => v.VIN == vin && v.UserId == userId, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);
}