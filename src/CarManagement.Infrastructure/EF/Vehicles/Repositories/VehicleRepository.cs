using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Repositories;
using CarManagement.Core.Vehicles.Repositories.DTO;

namespace CarManagement.Infrastructure.EF.Vehicles.Repositories;

internal sealed class VehicleRepository(CarManagementDbContext dbContext) : IVehicleRepository
{
    private readonly DbSet<Vehicle> _vehicles = dbContext.Vehicles;

    public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
        => await _vehicles.AddAsync(vehicle, cancellationToken);

    public async Task<bool> ExistsAsync(Expression<Func<Vehicle, bool>> predicate, CancellationToken cancellationToken)
        => await _vehicles.AnyAsync(predicate, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);

    public async Task<Vehicle> GetAsync(Guid vehicleId, CancellationToken cancellationToken)
        => await _vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == vehicleId, cancellationToken);

    public Task<IQueryable<Vehicle>> GetVehiclesByUserId(Guid userId, CancellationToken cancellationToken)
        => Task.FromResult(_vehicles
            .AsNoTracking()
            .Where(v => v.UserId == userId));

    public Task DeleteAsync(Vehicle vehicle, CancellationToken cancellationToken)
        => Task.FromResult(_vehicles.Remove(vehicle));

    public Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken)
        => Task.FromResult(_vehicles.Update(vehicle));

    public async Task<bool> ExistsWithLicensePlateAsync(string licensePlate, Guid vehicleId,
        CancellationToken cancellationToken)
        => await _vehicles
            .AnyAsync(v => v.LicensePlate == licensePlate && v.Id != vehicleId, cancellationToken);

    public async Task<bool> ExistsWithVinAsync(string vin, Guid vehicleId, CancellationToken cancellationToken)
        => await _vehicles
            .AnyAsync(v => v.VIN == vin && v.Id != vehicleId, cancellationToken);
}