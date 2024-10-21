using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Core.Vehicles.Repositories;

public interface IInspectionRepository
{
    Task<IQueryable<Inspection>> GetInspectionsAsync(Guid serviceBookId, CancellationToken cancellationToken);
}