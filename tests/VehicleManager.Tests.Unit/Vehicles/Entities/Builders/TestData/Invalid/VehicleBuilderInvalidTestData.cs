using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Tests.Unit.Vehicles.Entities.Builders.TestData.Invalid;

internal class VehicleBuilderInvalidTestData : TheoryData<VehicleBuilderParams>
{
    public VehicleBuilderInvalidTestData()
    {
        Add(new VehicleBuilderParams());

        Add(new VehicleBuilderParams
        {
            Brand = "Peugeot",
            Model = "308SW",
            Year = 2021,
            EngineCapacity = 1.6,
            EnginePower = 120,
            LicensePlate = null,
            VIN = "",
            FuelType = FuelType.Diesel,
            GearboxType = GearboxType.Automatic,
            VehicleType = VehicleType.Car
        });

        Add(new VehicleBuilderParams
        {
            Brand = "   ",
            Model = null,
            Year = 2021,
            EngineCapacity = 1.6,
            EnginePower = 120,
            LicensePlate = "KTA94969",
            VIN = null,
            FuelType = FuelType.Diesel,
            GearboxType = GearboxType.Automatic,
            VehicleType = VehicleType.Car
        });
    }
}