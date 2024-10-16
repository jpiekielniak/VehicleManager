using CarManagement.Core.Vehicles.Entities.Enums;
using DriveType = CarManagement.Core.Vehicles.Entities.Enums.DriveType;

namespace CarManagement.Core.Vehicles.Entities.Builders;

public class MotorcycleBuilder : VehicleBuilder<Motorcycle>
{
    
    public MotorcycleBuilder()
    {}

    public MotorcycleBuilder(Motorcycle vehicle)
    {
        Vehicle = vehicle;
    }
    public MotorcycleBuilder WithMotorcycleType(MotorcycleType motorcycleType)
    {
        if (!Enum.IsDefined(typeof(MotorcycleType), motorcycleType))
        {
            throw new ArgumentException("Invalid motorcycle type.");
        }

        Vehicle.MotorcycleType = motorcycleType;
        return this;
    }

    public MotorcycleBuilder WithSuspensionType(SuspensionType suspensionType)
    {
        if (!Enum.IsDefined(typeof(SuspensionType), suspensionType))
        {
            throw new ArgumentException("Invalid suspension type.");
        }

        Vehicle.SuspensionType = suspensionType;
        return this;
    }

    public MotorcycleBuilder WithDriveType(DriveType driveType)
    {
        if (!Enum.IsDefined(typeof(DriveType), driveType))
        {
            throw new ArgumentException("Invalid drive type.");
        }

        Vehicle.DriveType = driveType;
        return this;
    }

    public MotorcycleBuilder WithNumberOfCylinders(int numberOfCylinders)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numberOfCylinders);
        Vehicle.NumberOfCylinders = numberOfCylinders;
        return this;
    }

    public MotorcycleBuilder WithNumberOfGears(int numberOfGears)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numberOfGears);
        Vehicle.NumberOfGears = numberOfGears;
        return this;
    }

    public MotorcycleBuilder WithCoolingSystem(CoolingSystem coolingSystem)
    {
        if (!Enum.IsDefined(typeof(CoolingSystem), coolingSystem))
        {
            throw new ArgumentException("Invalid cooling system.");
        }

        Vehicle.CoolingSystem = coolingSystem;
        return this;
    }
}