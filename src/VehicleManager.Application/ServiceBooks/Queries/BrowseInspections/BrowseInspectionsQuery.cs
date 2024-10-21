using VehicleManager.Application.ServiceBooks.Queries.BrowseInspections.DTO;
using VehicleManager.Shared.Pagination;

namespace VehicleManager.Application.ServiceBooks.Queries.BrowseInspections;

public sealed record BrowseInspectionsQuery(Guid ServiceBookId, SieveModel SieveModel)
    : IRequest<PaginationResult<InspectionDto>>;