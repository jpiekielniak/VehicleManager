using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.Vehicles.Repositories;

internal sealed class ServiceBookRepository(VehicleManagerDbContext dbContext) : IServiceBookRepository
{
    private readonly DbSet<ServiceBook> _serviceBooks = dbContext.Set<ServiceBook>();

    public async Task<ServiceBook> GetAsync(Guid serviceBookId, CancellationToken cancellationToken)
        => await _serviceBooks
            .Include(sb => sb.Services)
            .Include(sb => sb.Inspections)
            .AsSplitQuery()
            .FirstOrDefaultAsync(sb => sb.Id == serviceBookId, cancellationToken);

    public async Task UpdateAsync(ServiceBook serviceBook, CancellationToken cancellationToken)
    {
        foreach (var entity in serviceBook.Inspections)
        {
            if (dbContext.Entry(entity).State is EntityState.Detached or EntityState.Modified)
            {
                dbContext.Entry(entity).State = EntityState.Added;
            }
        }

        await Task.FromResult(_serviceBooks.Update(serviceBook));
    }


    public Task SaveChangesAsync(CancellationToken cancellationToken)
        => dbContext.SaveChangesAsync(cancellationToken);
}