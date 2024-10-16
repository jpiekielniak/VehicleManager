using CarManagement.Core.Vehicles.Entities.Builders;
using CarManagement.Core.Vehicles.Entities.Enums;
using DriveType = CarManagement.Core.Vehicles.Entities.Enums.DriveType;

namespace CarManagement.Core.Vehicles.Entities.Factories;

public class VehicleFactory : IVehicleFactory
{
    public T CreateVehicle<T>(
        string brand,
        string model,
        int year,
        string licensePlate,
        string vin,
        double engineCapacity,
        int enginePower,
        FuelType fuelType,
        Guid userId
    ) where T : Vehicle, new()
    {
        var builder = new VehicleBuilder<T>()
            .WithBrand(brand)
            .WithModel(model)
            .WithYear(year)
            .WithLicensePlate(licensePlate)
            .WithVIN(vin)
            .WithEngineCapacity(engineCapacity)
            .WithEnginePower(enginePower)
            .WithFuelType(fuelType)
            .WithOwner(userId);

        return builder.Build();
    }
}