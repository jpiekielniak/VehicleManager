using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle.DTO;
using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle;

public static class Extensions
{
    public static InsuranceDetailsDto AsDetailsDto(this Insurance insurance)
        => new(
            insurance.Id,
            insurance.Title,
            insurance.Provider,
            insurance.PolicyNumber,
            insurance.ValidFrom,
            insurance.ValidTo
        );
}