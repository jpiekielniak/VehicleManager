using VehicleManager.Application.Vehicles.Queries.BrowseInsurancesForVehicle;
using VehicleManager.Application.Vehicles.Queries.BrowseInsurancesForVehicle.DTO;
using VehicleManager.Core.Common.Pagination;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.Vehicles.Queries.BrowseInsurancesForVehicle;

internal sealed class BrowseInsurancesForVehicleQueryHandler(
    IVehicleRepository vehicleRepository,
    ISieveProcessor sieveProcessor
) : IRequestHandler<BrowseInsurancesForVehicleQuery, PaginationResult<InsuranceDto>>
{
    public async Task<PaginationResult<InsuranceDto>> Handle(BrowseInsurancesForVehicleQuery query,
        CancellationToken cancellationToken)
    {
        var vehicleExists = await vehicleRepository.ExistsAsync(v => v.Id == query.VehicleId, cancellationToken);

        if (!vehicleExists)
        {
            throw new VehicleNotFoundException(query.VehicleId);
        }

        var insurances = await vehicleRepository.GetInsurancesByVehicleIdAsync(query.VehicleId, cancellationToken);

        var sieveResult = await sieveProcessor
            .Apply(query.SieveModel, insurances)
            .Select(x => new InsuranceDto(x.Id, x.Title))
            .ToListAsync(cancellationToken);

        var totalCount = sieveResult.Count;

        var result = new PaginationResult<InsuranceDto>(
            sieveResult,
            totalCount,
            query.SieveModel.Page ?? 1,
            query.SieveModel.PageSize ?? 5
        );

        return result;
    }
}