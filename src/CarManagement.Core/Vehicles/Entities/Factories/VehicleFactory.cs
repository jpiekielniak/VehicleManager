using CarManagement.Core.Vehicles.Entities.Builders;
using CarManagement.Core.Vehicles.Entities.Enums;
using DriveType = CarManagement.Core.Vehicles.Entities.Enums.DriveType;

namespace CarManagement.Core.Vehicles.Entities.Factories;

internal sealed class VehicleFactory : IVehicleFactory
{
    public Vehicle CreateVehicle(
        string brand,
        string model,
        int year,
        string licensePlate,
        string vin,
        double engineCapacity,
        int enginePower,
        FuelType fuelType,
        Guid userId,
        VehicleType vehicleType
    )
    {
        var builder = new VehicleBuilder()
            .WithBrand(brand)
            .WithModel(model)
            .WithYear(year)
            .WithLicensePlate(licensePlate)
            .WithVIN(vin)
            .WithEngineCapacity(engineCapacity)
            .WithEnginePower(enginePower)
            .WithFuelType(fuelType)
            .WithOwner(userId)
            .WithVehicleType(vehicleType);

        return builder.Build();
    }
}