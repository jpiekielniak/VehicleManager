using VehicleManager.Shared.Pagination;
using VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles.DTO;

namespace VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;

public record BrowseCurrentLoggedUserVehiclesQuery(SieveModel SieveModel)
    : IRequest<PaginationResult<VehicleDto>>;