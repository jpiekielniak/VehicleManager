using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Core.Vehicles.Builders;

public class VehicleBuilder(Vehicle vehicle)
{
    public Vehicle Build() => vehicle;

    public VehicleBuilder() : this(Vehicle.Create())
    {
    }

    public VehicleBuilder WithBrand(string brand)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(brand);
        vehicle.Brand = brand;
        return this;
    }

    public VehicleBuilder WithModel(string model)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(model);
        vehicle.Model = model;
        return this;
    }

    public VehicleBuilder WithYear(int year)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(year);
        vehicle.Year = year;
        return this;
    }

    public VehicleBuilder WithLicensePlate(string licensePlate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(licensePlate);
        vehicle.LicensePlate = licensePlate;
        return this;
    }

    public VehicleBuilder WithVIN(string vin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(vin);
        vehicle.VIN = vin;
        return this;
    }

    public VehicleBuilder WithEngineCapacity(double engineCapacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(engineCapacity);
        vehicle.EngineCapacity = engineCapacity;
        return this;
    }

    public VehicleBuilder WithEnginePower(int enginePower)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(enginePower);
        vehicle.EnginePower = enginePower;
        return this;
    }

    public VehicleBuilder WithFuelType(FuelType fuelType)
    {
        if (!Enum.IsDefined(typeof(FuelType), fuelType))
        {
            throw new ArgumentException("Invalid fuel type.");
        }

        vehicle.FuelType = fuelType;
        return this;
    }

    public VehicleBuilder WithOwner(Guid userId)
    {
        vehicle.UserId = userId;
        return this;
    }

    public VehicleBuilder WithVehicleType(VehicleType vehicleType)
    {
        if (!Enum.IsDefined(typeof(VehicleType), vehicleType))
        {
            throw new ArgumentException("Invalid vehicle type.");
        }

        vehicle.VehicleType = vehicleType;
        return this;
    }

    public VehicleBuilder WithGearboxType(GearboxType gearboxType)
    {
        if (!Enum.IsDefined(typeof(GearboxType), gearboxType))
        {
            throw new ArgumentException("Invalid gearbox type.");
        }

        vehicle.GearboxType = gearboxType;
        return this;
    }

    public VehicleBuilder WithServiceBook(ServiceBook serviceBook)
    {
        ArgumentNullException.ThrowIfNull(serviceBook);
        vehicle.ServiceBook = serviceBook;
        return this;
    }
}