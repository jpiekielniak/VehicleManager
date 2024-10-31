namespace VehicleManager.Core.Vehicles.Entities.Enums;

public enum InspectionType : short
{
    [Display(Name = "Okresowy")] Periodic,
    [Display(Name = "Techniczny")] Technical,
    [Display(Name = "Emisyjny")] Emissions
}