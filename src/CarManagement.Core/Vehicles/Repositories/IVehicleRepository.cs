using CarManagement.Core.Vehicles.Entities;

namespace CarManagement.Core.Vehicles.Repositories;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string vin, Guid userId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}