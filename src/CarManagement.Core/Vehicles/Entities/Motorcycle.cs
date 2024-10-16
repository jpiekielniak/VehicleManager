using CarManagement.Core.Vehicles.Entities.Enums;
using DriveType = CarManagement.Core.Vehicles.Entities.Enums.DriveType;

namespace CarManagement.Core.Vehicles.Entities;

public class Motorcycle : Vehicle
{
    public MotorcycleType? MotorcycleType { get; set; }
    public SuspensionType? SuspensionType { get; set; }
    public DriveType? DriveType { get; set; }
    public int? NumberOfCylinders { get; set; }
    public int? NumberOfGears { get; set; }
    public CoolingSystem? CoolingSystem { get; set; }
}