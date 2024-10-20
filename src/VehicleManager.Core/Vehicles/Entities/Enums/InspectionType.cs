namespace VehicleManager.Core.Vehicles.Entities.Enums;

public enum InspectionType
{
    [Display(Name = "Okresowy")] Periodic,
    [Display(Name = "Techniczny")] Technical,
    [Display(Name = "Emisyjny")] Emissions
}