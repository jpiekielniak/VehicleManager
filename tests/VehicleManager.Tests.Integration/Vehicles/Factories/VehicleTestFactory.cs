using VehicleManager.Application.Vehicles.Commands.AddInsurance;
using VehicleManager.Application.Vehicles.Commands.ChangeVehicleInformation;
using VehicleManager.Application.Vehicles.Commands.CreateVehicle;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Tests.Integration.Vehicles.Factories;

internal class VehicleTestFactory
{
    private readonly Faker _faker = new();

    internal CreateVehicleCommand CreateVehicleCommand()
        => new(
            _faker.Vehicle.Manufacturer(),
            _faker.Vehicle.Model(),
            _faker.Date.Random.Int(1980, 2024),
            GenerateLicensePlate(),
            _faker.Vehicle.Vin(),
            _faker.Random.Int(900, 5400),
            _faker.Random.Int(60, 300),
            _faker.PickRandom<FuelType>(),
            _faker.PickRandom<GearboxType>(),
            _faker.PickRandom<VehicleType>()
        );

    public Vehicle CreateVehicle(Guid userId = default)
    {
        var vehicle = new VehicleBuilder()
            .WithBrand(_faker.Vehicle.Manufacturer())
            .WithModel(_faker.Vehicle.Model())
            .WithYear(_faker.Date.Random.Int(1980, 2024))
            .WithLicensePlate(GenerateLicensePlate())
            .WithVIN(_faker.Vehicle.Vin())
            .WithEngineCapacity(_faker.Random.Int(900, 5400))
            .WithEnginePower(_faker.Random.Int(60, 300))
            .WithFuelType(_faker.PickRandom<FuelType>())
            .WithGearboxType(_faker.PickRandom<GearboxType>())
            .WithVehicleType(_faker.PickRandom<VehicleType>())
            .WithServiceBook(ServiceBook.Create())
            .WithOwner(userId == Guid.Empty ? FastGuid.NewGuid() : userId)
            .Build();

        var image = CreateImage(vehicle.Id);

        return new VehicleBuilder(vehicle)
            .WithImage(image)
            .Build();
    }

    private Image CreateImage(Guid vehicleId = default)
        => Image.Create(
            vehicleId == Guid.Empty ? FastGuid.NewGuid() : vehicleId,
            _faker.Image.PlaceholderUrl(100, 100),
            _faker.Lorem.Sentence()
        );

    private string GenerateLicensePlate()
        => _faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
           + _faker.Random.String2(5, "0123456789");

    public User CreateUser()
        => new UserBuilder()
            .WithEmail(_faker.Internet.Email())
            .WithPassword(_faker.Internet.Password())
            .WithRole(Role.User)
            .Build();

    public ChangeVehicleInformationCommand CreateChangeVehicleInformationCommand(Guid vehicleId)
        => new(
            _faker.Vehicle.Manufacturer(),
            _faker.Vehicle.Model(),
            _faker.Date.Random.Int(1980, 2024),
            GenerateLicensePlate(),
            _faker.Vehicle.Vin(),
            _faker.Random.Int(900, 5400),
            _faker.Random.Int(80, 300),
            _faker.PickRandom<FuelType>(),
            _faker.PickRandom<GearboxType>(),
            _faker.PickRandom<VehicleType>())
        {
            VehicleId = vehicleId
        };

    public List<Vehicle> CreateVehicles(Guid userId, int numberOfVehicles)
    {
        var vehicles = new List<Vehicle>();

        for (var i = 0; i < numberOfVehicles; i++)
        {
            vehicles.Add(CreateVehicle(userId));
        }

        return vehicles;
    }

    public AddInsuranceCommand CreateAddInsuranceCommand(Guid vehicleId = default)
        => new(
            _faker.Lorem.Sentence(),
            _faker.Company.CompanyName(),
            _faker.Random.String2(10, "0123456789"),
            _faker.Date.Recent(),
            _faker.Date.Future())
        {
            VehicleId = vehicleId == Guid.Empty ? FastGuid.NewGuid()  : vehicleId
        };

    public Insurance CreateInsurance(Vehicle vehicle)
        => new InsuranceBuilder()
            .WithTitle(_faker.Lorem.Sentence())
            .WithProvider(_faker.Company.CompanyName())
            .WithPolicyNumber(_faker.Random.String2(10, "0123456789"))
            .WithValidFrom(_faker.Date.Recent())
            .WithValidTo(_faker.Date.Future())
            .WithVehicle(vehicle)
            .Build();

    public List<Insurance> CreateInsurances(Vehicle vehicle, int numberOfInsurances)
    {
        var insurances = new List<Insurance>();

        for (var i = 0; i < numberOfInsurances; i++)
        {
            insurances.Add(CreateInsurance(vehicle));
        }

        return insurances;
    }
}