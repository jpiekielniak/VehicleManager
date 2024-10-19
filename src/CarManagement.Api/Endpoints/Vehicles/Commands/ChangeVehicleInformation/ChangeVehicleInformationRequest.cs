using CarManagement.Application.Vehicles.Commands.ChangeVehicleInformation;

namespace CarManagement.Api.Endpoints.Vehicles.Commands.ChangeVehicleInformation;

internal sealed class ChangeVehicleInformationRequest
{
    [FromRoute(Name = "vehicleId")] public Guid VehicleId { get; init; }
    [FromBody] public ChangeVehicleInformationCommand Command { get; init; } = default;
}