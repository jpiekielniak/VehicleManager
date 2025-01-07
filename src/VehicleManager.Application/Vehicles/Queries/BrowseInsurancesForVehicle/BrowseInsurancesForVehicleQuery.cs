using VehicleManager.Application.Vehicles.Queries.BrowseInsurancesForVehicle.DTO;
using VehicleManager.Core.Common.Pagination;

namespace VehicleManager.Application.Vehicles.Queries.BrowseInsurancesForVehicle;

public record BrowseInsurancesForVehicleQuery(Guid VehicleId, SieveModel SieveModel)
    : IRequest<PaginationResult<InsuranceDto>>;