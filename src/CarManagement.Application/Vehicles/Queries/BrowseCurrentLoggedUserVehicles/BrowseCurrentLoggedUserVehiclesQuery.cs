using CarManagement.Core.Vehicles.Repositories.DTO;
using CarManagement.Shared.Pagination;

namespace CarManagement.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;

public record BrowseCurrentLoggedUserVehiclesQuery(SieveModel SieveModel)
    : IRequest<PaginationResult<VehicleDto>>;