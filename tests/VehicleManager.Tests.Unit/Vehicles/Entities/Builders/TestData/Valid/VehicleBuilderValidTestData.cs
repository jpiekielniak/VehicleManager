using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Tests.Unit.Vehicles.Entities.Builders.TestData.Valid;

internal class VehicleBuilderValidTestData : TheoryData<VehicleBuilderParams>
{
    public VehicleBuilderValidTestData()
    {
        Add(new VehicleBuilderParams
        {
            Brand = "Peugeot",
            Model = "308SW",
            Year = 2021,
            EngineCapacity = 1.6,
            EnginePower = 120,
            LicensePlate = "KTA94969",
            VIN = "KIU4365928LOS9120",
            FuelType = FuelType.Diesel,
            GearboxType = GearboxType.Automatic,
            VehicleType = VehicleType.Car
        });

        Add(new VehicleBuilderParams
        {
            Brand = "Audi",
            Model = "A4",
            Year = 2000,
            EngineCapacity = 2.0,
            EnginePower = 150,
            LicensePlate = "KTA7432K",
            VIN = "AL98OU65203546786",
            FuelType = FuelType.Gasoline,
            GearboxType = GearboxType.Manual,
            VehicleType = VehicleType.Car
        });

        Add(new VehicleBuilderParams
        {
            Brand = "Suzuki",
            Model = "GSX-R",
            Year = 2020,
            EngineCapacity = 1000,
            EnginePower = 200,
            LicensePlate = "KTA 9874",
            VIN = "OK3465781024KL23",
            FuelType = FuelType.Gasoline,
            GearboxType = GearboxType.Manual,
            VehicleType = VehicleType.Motorcycle
        });
    }
}