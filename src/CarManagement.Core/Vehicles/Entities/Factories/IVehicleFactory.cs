using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Core.Vehicles.Entities.Factories;

public interface IVehicleFactory
{
    Vehicle CreateVehicle(
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
    );
}