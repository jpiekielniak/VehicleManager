using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.Vehicles.Repositories;

internal sealed class VehicleRepository(VehicleManagerDbContext dbContext) : IVehicleRepository
{
    private readonly DbSet<Vehicle> _vehicles = dbContext.Set<Vehicle>();

    public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
        => await _vehicles.AddAsync(vehicle, cancellationToken);

    public async Task<bool> ExistsAsync(Expression<Func<Vehicle, bool>> predicate, CancellationToken cancellationToken)
        => await _vehicles.AnyAsync(predicate, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);

    public async Task<Vehicle> GetAsync(Guid vehicleId, CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = _vehicles.AsQueryable();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync(v => v.Id == vehicleId, cancellationToken);
    }


    public Task<IQueryable<Vehicle>> GetVehiclesByUserId(Guid userId, CancellationToken cancellationToken)
        => Task.FromResult(_vehicles
            .AsNoTracking()
            .Where(v => v.UserId == userId));

    public Task DeleteAsync(Vehicle vehicle, CancellationToken cancellationToken)
        => Task.FromResult(_vehicles.Remove(vehicle));

    public async Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        var insurances = vehicle.Insurances;

        foreach (var insurance in insurances)
        {
            if (dbContext.Entry(insurance).State is EntityState.Detached or EntityState.Modified)
            {
                dbContext.Entry(insurance).State = EntityState.Added;
            }
        }

        await Task.FromResult(dbContext.Update(vehicle));
    }
}