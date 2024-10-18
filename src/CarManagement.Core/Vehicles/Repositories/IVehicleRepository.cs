using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Repositories.DTO;

namespace CarManagement.Core.Vehicles.Repositories;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string vin, Guid userId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<Vehicle> GetAsync(Guid vehicleId, CancellationToken cancellationToken);
    Task<IQueryable<VehicleDto>> GetVehiclesByUserId(Guid userId, CancellationToken cancellationToken);
}