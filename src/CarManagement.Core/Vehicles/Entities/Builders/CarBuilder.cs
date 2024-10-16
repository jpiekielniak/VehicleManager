using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Core.Vehicles.Entities.Builders;

public class CarBuilder : VehicleBuilder<Car>
{
    public CarBuilder()
    {
    }

    public CarBuilder(Car vehicle)
    {
        Vehicle = vehicle;
    }

    public CarBuilder WithNumberOfDoors(int numberOfDoors)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numberOfDoors);
        Vehicle.NumberOfDoors = numberOfDoors;
        return this;
    }

    public CarBuilder WithBodyType(BodyType bodyType)
    {
        if (!Enum.IsDefined(typeof(BodyType), bodyType))
        {
            throw new ArgumentException("Invalid body type.");
        }

        Vehicle.BodyType = bodyType;
        return this;
    }

    public CarBuilder WithGearboxType(GearboxType gearboxType)
    {
        if (!Enum.IsDefined(typeof(GearboxType), gearboxType))
        {
            throw new ArgumentException("Invalid gearbox type.");
        }

        Vehicle.GearboxType = gearboxType;
        return this;
    }
}