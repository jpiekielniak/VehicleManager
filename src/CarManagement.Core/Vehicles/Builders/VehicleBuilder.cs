using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Core.Vehicles.Builders;

public class VehicleBuilder
{
    private readonly Vehicle _vehicle = Vehicle.Create();
    public Vehicle Build() => _vehicle;

    public VehicleBuilder WithBrand(string brand)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(brand);
        _vehicle.Brand = brand;
        return this;
    }

    public VehicleBuilder WithModel(string model)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(model);
        _vehicle.Model = model;
        return this;
    }

    public VehicleBuilder WithYear(int year)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(year);
        _vehicle.Year = year;
        return this;
    }

    public VehicleBuilder WithLicensePlate(string licensePlate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(licensePlate);
        _vehicle.LicensePlate = licensePlate;
        return this;
    }

    public VehicleBuilder WithVIN(string vin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(vin);
        _vehicle.VIN = vin;
        return this;
    }

    public VehicleBuilder WithEngineCapacity(double engineCapacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(engineCapacity);
        _vehicle.EngineCapacity = engineCapacity;
        return this;
    }

    public VehicleBuilder WithEnginePower(int enginePower)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(enginePower);
        _vehicle.EnginePower = enginePower;
        return this;
    }

    public VehicleBuilder WithFuelType(FuelType fuelType)
    {
        if (!Enum.IsDefined(typeof(FuelType), fuelType))
        {
            throw new ArgumentException("Invalid fuel type.");
        }

        _vehicle.FuelType = fuelType;
        return this;
    }

    public VehicleBuilder WithOwner(Guid userId)
    {
        _vehicle.UserId = userId;
        return this;
    }

    public VehicleBuilder WithVehicleType(VehicleType vehicleType)
    {
        if (!Enum.IsDefined(typeof(VehicleType), vehicleType))
        {
            throw new ArgumentException("Invalid vehicle type.");
        }

        _vehicle.VehicleType = vehicleType;
        return this;
    }

    public VehicleBuilder WithServiceBook(ServiceBook serviceBook)
    {
        ArgumentNullException.ThrowIfNull(serviceBook);
        _vehicle.ServiceBook = serviceBook;
        return this;
    }
}