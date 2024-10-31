using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle;
using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle.DTO;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.Vehicles.Queries.GetInsuranceDetailsForVehicle;

internal sealed class GetInsuranceDetailsForVehicleQueryHandler(
    IVehicleRepository vehicleRepository
) : IRequestHandler<GetInsuranceDetailsForVehicleQuery, InsuranceDetailsDto>
{
    public async Task<InsuranceDetailsDto> Handle(GetInsuranceDetailsForVehicleQuery query,
        CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetAsync(query.VehicleId, cancellationToken, asNoTracking: true)
                      ?? throw new VehicleNotFoundException(query.VehicleId);

        var insurance = vehicle.Insurances.FirstOrDefault(i => i.Id == query.InsuranceId)
                        ?? throw new InsuranceNotFoundException(query.InsuranceId);

        return insurance.AsDetailsDto();
    }
}