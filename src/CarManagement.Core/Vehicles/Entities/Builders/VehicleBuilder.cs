using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Core.Vehicles.Entities.Builders;

public class VehicleBuilder<T> where T : Vehicle, new()
{
    protected T Vehicle = new();

    public VehicleBuilder<T> WithBrand(string brand)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(brand);
        Vehicle.Brand = brand;
        return this;
    }

    public VehicleBuilder<T> WithModel(string model)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(model);
        Vehicle.Model = model;
        return this;
    }

    public VehicleBuilder<T> WithYear(int year)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(year);
        Vehicle.Year = year;
        return this;
    }

    public VehicleBuilder<T> WithLicensePlate(string licensePlate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(licensePlate);
        Vehicle.LicensePlate = licensePlate;
        return this;
    }

    public VehicleBuilder<T> WithVIN(string vin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(vin);
        Vehicle.VIN = vin;
        return this;
    }

    public VehicleBuilder<T> WithEngineCapacity(double engineCapacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(engineCapacity);
        Vehicle.EngineCapacity = engineCapacity;
        return this;
    }

    public VehicleBuilder<T> WithEnginePower(int enginePower)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(enginePower);
        Vehicle.EnginePower = enginePower;
        return this;
    }

    public VehicleBuilder<T> WithFuelType(FuelType fuelType)
    {
        if (!Enum.IsDefined(typeof(FuelType), fuelType))
        {
            throw new ArgumentException("Invalid fuel type.");
        }

        Vehicle.FuelType = fuelType;
        return this;
    }

    public VehicleBuilder<T> WithOwner(Guid userId)
    {
        Vehicle.UserId = userId;
        return this;
    }

    public T Build() => Vehicle;
}