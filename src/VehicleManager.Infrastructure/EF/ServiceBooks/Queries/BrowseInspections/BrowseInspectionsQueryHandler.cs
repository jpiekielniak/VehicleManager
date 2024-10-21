using VehicleManager.Application.ServiceBooks.Queries.BrowseInspections;
using VehicleManager.Application.ServiceBooks.Queries.BrowseInspections.DTO;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Shared.Pagination;

namespace VehicleManager.Infrastructure.EF.ServiceBooks.Queries.BrowseInspections;

internal sealed class BrowseInspectionsQueryHandler(
    IServiceBookRepository serviceBookRepository,
    IInspectionRepository inspectionRepository,
    ISieveProcessor sieveProcessor)
    : IRequestHandler<BrowseInspectionQuery, PaginationResult<InspectionDto>>
{
    public async Task<PaginationResult<InspectionDto>> Handle(BrowseInspectionQuery query,
        CancellationToken cancellationToken)
    {
        var isServiceBookExists = await serviceBookRepository
            .ExistsAsync(query.ServiceBookId, cancellationToken);

        if (!isServiceBookExists)
        {
            throw new ServiceBookNotFoundException(query.ServiceBookId);
        }

        var inspections = await inspectionRepository
            .GetInspectionsAsync(query.ServiceBookId, cancellationToken);

        var sieveResult = await sieveProcessor
            .Apply(query.SieveModel, inspections)
            .Select(i => new InspectionDto(i.Id, i.Title))
            .ToListAsync(cancellationToken);

        var totalCount = sieveResult.Count;

        var result = new PaginationResult<InspectionDto>(
            sieveResult,
            totalCount,
            query.SieveModel.PageSize ?? 5,
            query.SieveModel.Page ?? 1
        );

        return result;
    }
}