using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Core.Vehicles.Repositories;

public interface IServiceRepository
{
    Task<IQueryable<Service>> GetServicesAsync(Guid serviceBookId, CancellationToken cancellationToken);
}