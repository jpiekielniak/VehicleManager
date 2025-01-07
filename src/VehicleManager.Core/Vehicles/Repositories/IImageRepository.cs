using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Core.Vehicles.Repositories;

public interface IImageRepository
{
    Task AddAsync(Image image, CancellationToken cancellationToken);
    Task GetAsync(Guid imageId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}