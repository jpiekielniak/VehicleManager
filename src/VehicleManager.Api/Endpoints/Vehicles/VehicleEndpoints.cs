namespace VehicleManager.Api.Endpoints.Vehicles;

public static class VehicleEndpoints
{
    public static string Vehicles => "Vehicles";
    public static string BasePath => $"{RootPath.BasePath}/{Vehicles.ToLowerInvariant()}";
    public static string VehicleById => $"{BasePath}/{{vehicleId:guid}}";
    public static string Insurance => $"{VehicleById}/insurances";
    public static string InsuranceById => $"{Insurance}/{{insuranceId:guid}}";
}