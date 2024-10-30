using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.ServiceBooks.Repositories;

internal sealed class ServiceBookRepository(VehicleManagerDbContext dbContext) : IServiceBookRepository
{
    private readonly DbSet<ServiceBook> _serviceBooks = dbContext.Set<ServiceBook>();

    public async Task<ServiceBook> GetAsync(Guid serviceBookId, CancellationToken cancellationToken,
        bool asNoTracking = false)
    {
        var query = _serviceBooks
            .Include(sb => sb.Services)
            .ThenInclude(s => s.Costs)
            .Include(sb => sb.Inspections)
            .AsSplitQuery();

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync(sb => sb.Id == serviceBookId, cancellationToken);
    }


    public async Task UpdateAsync(ServiceBook serviceBook, CancellationToken cancellationToken)
    {
        UpdateEntitiesState(serviceBook.Inspections);

        foreach (var service in serviceBook.Services)
        {
            UpdateEntitiesState(service.Costs);
            UpdateEntityState(service);
        }

        await Task.FromResult(_serviceBooks.Update(serviceBook));
    }

    public async Task<bool> ExistsAsync(Guid serviceBookId, CancellationToken cancellationToken)
        => await _serviceBooks.AnyAsync(sb => sb.Id == serviceBookId, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken)
        => dbContext.SaveChangesAsync(cancellationToken);

    private void UpdateEntitiesState<T>(IEnumerable<T> entities) where T : class
    {
        foreach (var entity in entities)
        {
            UpdateEntityState(entity);
        }
    }

    private void UpdateEntityState<T>(T entity) where T : class
    {
        if (dbContext.Entry(entity).State is EntityState.Detached or EntityState.Modified)
        {
            dbContext.Entry(entity).State = EntityState.Added;
        }
    }
}