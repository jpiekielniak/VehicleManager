using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Core.Vehicles.Repositories;

public interface IServiceBookRepository
{
    Task<ServiceBook> GetAsync(Guid serviceBookId, CancellationToken cancellationToken, bool asNoTracking = false);
    Task UpdateAsync(ServiceBook serviceBook, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid serviceBookId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}