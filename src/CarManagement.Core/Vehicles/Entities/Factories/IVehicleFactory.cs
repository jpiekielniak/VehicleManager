using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Core.Vehicles.Entities.Factories;

public interface IVehicleFactory
{
    T CreateVehicle<T>(
        string brand,
        string model,
        int year,
        string licensePlate,
        string vin,
        double engineCapacity,
        int enginePower,
        FuelType fuelType,
        Guid userId) where T : Vehicle, new();
}