namespace VehicleManager.Api.Endpoints.Vehicles.Commands.DeleteInsurance;

internal class DeleteInsuranceRequest
{
    [FromRoute(Name = "vehicleId")] public Guid VehicleId { get; init; }
    [FromRoute(Name = "insuranceId")] public Guid InsuranceId { get; init; }
}