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

    public async Task<List<Inspection>> GetExpiringInspectionsAsync(CancellationToken cancellationToken)
    {
        const int daysBeforeExpiration = 7;
        var today = DateTime.UtcNow.Date;

        return await _inspections
            .AsNoTracking()
            .Include(i => i.ServiceBook)
            .ThenInclude(i => i.Vehicle)
            .ThenInclude(i => i.User)
            .Where(i =>
                !i.ReminderSent &&
                i.PerformDate.Value.AddYears(1) >= today &&
                i.PerformDate.Value.AddYears(1) <= today.AddDays(daysBeforeExpiration)
            )
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Inspection inspection, CancellationToken cancellationToken)
    {
        _inspections.Update(inspection);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}