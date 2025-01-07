using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Tests.Unit.Vehicles.Entities.Builders.TestData.Valid;

internal class VehicleBuilderValidTestData : TheoryData<VehicleBuilderParams>
{
    private readonly Faker _faker = new();

    public VehicleBuilderValidTestData()
    {
        Add(new VehicleBuilderParams
        {
            Brand = _faker.Vehicle.Manufacturer(),
            Model = _faker.Vehicle.Model(),
            Year = _faker.Date.Past().Year,
            EngineCapacity = _faker.Random.Int(1000, 5000),
            EnginePower = _faker.Random.Int(50, 500),
            LicensePlate = GenerateLicensePlate(),
            VIN = _faker.Vehicle.Vin(),
            FuelType = FuelType.Diesel,
            GearboxType = GearboxType.Automatic,
            VehicleType = VehicleType.Car
        });

        Add(new VehicleBuilderParams
        {
            Brand = "Audi",
            Model = "A4",
            Year = 2000,
            EngineCapacity = 1995,
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

    private string GenerateLicensePlate()
        => _faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
           + _faker.Random.String2(5, "0123456789");
}