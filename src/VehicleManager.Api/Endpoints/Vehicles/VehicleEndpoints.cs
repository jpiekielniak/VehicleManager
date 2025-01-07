namespace VehicleManager.Api.Endpoints.Vehicles;

public static class VehicleEndpoints
{
    public static string Vehicles => "Vehicles";
    public static string BasePath => $"{RootPath.BasePath}/{Vehicles.ToLowerInvariant()}";
    public static string VehicleById => $"{BasePath}/{{vehicleId:guid}}";
    public static string Insurances => $"{VehicleById}/insurances";
    public static string InsuranceById => $"{Insurances}/{{insuranceId:guid}}";
    public static string AddImageToVehicle => $"{VehicleById}/image";
}