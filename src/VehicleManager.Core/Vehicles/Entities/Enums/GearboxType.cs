namespace VehicleManager.Core.Vehicles.Entities.Enums;

public enum GearboxType : short
{
    [Display(Name = "Manualna")] Manual,
    
    [Display(Name = "Automatyczna")] Automatic,
    
    [Display(Name = "Pół automatyczna")] SemiAutomatic
}