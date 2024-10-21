using VehicleManager.Application.ServiceBooks.Queries.BrowseServices;
using VehicleManager.Application.ServiceBooks.Queries.BrowseServices.DTO;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Shared.Pagination;

namespace VehicleManager.Infrastructure.EF.ServiceBooks.Queries.BrowseServices;

internal sealed class BrowseServicesQueryHandler(
    IServiceBookRepository serviceBookRepository,
    IServiceRepository serviceRepository,
    ISieveProcessor sieveProcessor
) : IRequestHandler<BrowseServicesQuery, PaginationResult<ServiceDto>>
{
    public async Task<PaginationResult<ServiceDto>> Handle(BrowseServicesQuery query,
        CancellationToken cancellationToken)
    {
        var serviceBookExists = await serviceBookRepository
            .ExistsAsync(query.ServiceBookId, cancellationToken);

        if (!serviceBookExists)
        {
            throw new ServiceBookNotFoundException(query.ServiceBookId);
        }

        var services = await serviceRepository.GetServicesAsync(query.ServiceBookId, cancellationToken);

        var sieveResult = await sieveProcessor
            .Apply(query.SieveModel, services)
            .Select(x => new ServiceDto(x.Id, x.Title))
            .ToListAsync(cancellationToken);

        var totalCount = sieveResult.Count;

        var result = new PaginationResult<ServiceDto>(
            sieveResult,
            totalCount,
            query.SieveModel.Page ?? 1,
            query.SieveModel.PageSize ?? 5
        );

        return result;
    }
}