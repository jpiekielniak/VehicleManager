using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Core.Vehicles.Repositories;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Expression<Func<Vehicle, bool>> predicate, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<Vehicle> GetAsync(Guid vehicleId, CancellationToken cancellationToken);
    Task<IQueryable<Vehicle>> GetVehiclesByUserId(Guid userId, CancellationToken cancellationToken);
    Task DeleteAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken);
}