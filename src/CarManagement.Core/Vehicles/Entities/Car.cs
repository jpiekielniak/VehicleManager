using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Core.Vehicles.Entities;

public class Car : Vehicle
{
    public int NumberOfDoors { get; set; }
    public BodyType BodyType { get; set; }
    public GearboxType? GearboxType { get; set; }
    
}