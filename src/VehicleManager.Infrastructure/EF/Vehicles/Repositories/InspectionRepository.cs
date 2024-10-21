using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.Vehicles.Repositories;

internal sealed class InspectionRepository(VehicleManagerDbContext dbContext) : IInspectionRepository
{
    private readonly DbSet<Inspection> _inspections = dbContext.Set<Inspection>();

    public async Task<IQueryable<Inspection>> GetInspectionsAsync(Guid serviceBookId,
        CancellationToken cancellationToken)
        => await Task.FromResult(_inspections
            .AsNoTracking()
            .Where(i => i.ServiceBookId == serviceBookId));
}