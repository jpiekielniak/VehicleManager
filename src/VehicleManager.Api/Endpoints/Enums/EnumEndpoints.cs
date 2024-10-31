namespace VehicleManager.Api.Endpoints.Enums;

public static class EnumEndpoints
{
    private static string BasePath => $"{RootPath.BasePath}";
    public static string Enums => "Enums";
    public static string GearboxTypes => $"{BasePath}/gearbox-types";
    public static string VehicleTypes => $"{BasePath}/vehicle-types";
    public static string FuelTypes => $"{BasePath}/fuel-types";
    public static string InspectionTypes => $"{BasePath}/inspection-types";
}