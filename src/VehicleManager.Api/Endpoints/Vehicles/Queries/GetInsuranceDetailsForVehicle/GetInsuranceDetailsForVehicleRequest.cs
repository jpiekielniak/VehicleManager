namespace VehicleManager.Api.Endpoints.Vehicles.Queries.GetInsuranceDetailsForVehicle;

internal sealed class GetInsuranceDetailsForVehicleRequest
{
    [FromRoute(Name = "vehicleId")] public Guid VehicleId { get; init; }
    [FromRoute(Name = "insuranceId")] public Guid InsuranceId { get; init; }
}