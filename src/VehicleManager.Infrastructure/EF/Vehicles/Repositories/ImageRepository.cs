using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.Vehicles.Repositories;

internal sealed class ImageRepository(VehicleManagerDbContext dbContext) : IImageRepository
{
    private readonly DbSet<Image> _images = dbContext.Set<Image>();

    public async Task AddAsync(Image image, CancellationToken cancellationToken)
        => await _images.AddAsync(image, cancellationToken);

    public async Task GetAsync(Guid imageId, CancellationToken cancellationToken)
        => await _images
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == imageId, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);

    public Task DeleteAsync(Image image, CancellationToken cancellationToken)
    {
        _images.Remove(image);
        return Task.CompletedTask;
    }
}