namespace VehicleManager.Core.Vehicles.Entities.Enums;

public enum FuelType : short
{
    [Display(Name = "Benzyna")] Gasoline,

    [Display(Name = "Diesel")] Diesel,

    [Display(Name = "Elektryczny")] Electric,

    [Display(Name = "Hybrydowy")] Hybrid
}