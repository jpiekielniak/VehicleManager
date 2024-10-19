using CarManagement.Core.Vehicles.Entities;

namespace CarManagement.Core.Vehicles.Repositories;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Expression<Func<Vehicle, bool>> predicate, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<Vehicle> GetAsync(Guid vehicleId, CancellationToken cancellationToken);
    Task<IQueryable<Vehicle>> GetVehiclesByUserId(Guid userId, CancellationToken cancellationToken);
    Task DeleteAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task<bool> ExistsWithLicensePlateAsync(string licensePlate, Guid vehicleId, CancellationToken cancellationToken);
    Task<bool> ExistsWithVinAsync(string vin, Guid vehicleId, CancellationToken cancellationToken);
}