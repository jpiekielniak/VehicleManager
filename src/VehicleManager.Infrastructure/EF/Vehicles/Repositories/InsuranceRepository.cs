using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.Vehicles.Repositories;

internal sealed class InsuranceRepository(VehicleManagerDbContext dbContext) : IInsuranceRepository
{
    private readonly DbSet<Insurance> _insurances = dbContext.Set<Insurance>();

    public async Task UpdateAsync(Insurance insurance, CancellationToken cancellationToken)
    {
        _insurances.Update(insurance);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Insurance>> GetExpiringInsurancesAsync(CancellationToken cancellationToken)
    {
        var today = DateTimeOffset.UtcNow.Date;
        const int daysBeforeExpiration = 7;

        return await _insurances
            .AsNoTracking()
            .Include(i => i.Vehicle)
            .ThenInclude(i => i.User)
            .Where(i =>
                !i.ReminderSent &&
                (i.ValidTo.Date - today).TotalDays <= daysBeforeExpiration &&
                (i.ValidTo.Date - today).TotalDays >= 0)
            .ToListAsync(cancellationToken);
    }

    private async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);
}