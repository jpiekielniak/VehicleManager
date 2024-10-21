using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.Vehicles.Repositories;

internal sealed class ServiceRepository(VehicleManagerDbContext dbContext) : IServiceRepository
{
    private readonly DbSet<Service> _services = dbContext.Set<Service>();

    public async Task<IQueryable<Service>> GetServicesAsync(Guid serviceBookId,
        CancellationToken cancellationToken) =>
        await Task.FromResult(
            _services
                .AsNoTracking()
                .Where(s => s.ServiceBookId == serviceBookId)
        );
}