using VehicleManager.Application.Vehicles.Commands.AddInsurance;

namespace VehicleManager.Api.Endpoints.Vehicles.Commands.AddInsurance;

internal class AddInsuranceRequest
{
    [FromRoute(Name = "vehicleId")] public Guid VehicleId { get; init; }
    [FromBody] public AddInsuranceCommand Command { get; init; } = default!;
}