using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Core.Vehicles.Repositories;

public interface IInsuranceRepository
{
    Task UpdateAsync(Insurance insurance, CancellationToken cancellationToken);
    Task<List<Insurance>> GetExpiringInsurancesAsync(CancellationToken cancellationToken);
}