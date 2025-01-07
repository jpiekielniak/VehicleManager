using VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles.DTO;
using VehicleManager.Core.Common.Pagination;

namespace VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;

public record BrowseCurrentLoggedUserVehiclesQuery(SieveModel SieveModel)
    : IRequest<PaginationResult<VehicleDto>>;