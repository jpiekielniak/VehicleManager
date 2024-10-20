using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Core.Vehicles.Repositories;

public interface IServiceBookRepository
{
    Task<ServiceBook> GetAsync(Guid serviceBookId, CancellationToken cancellationToken);
    Task UpdateAsync(ServiceBook serviceBook, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}