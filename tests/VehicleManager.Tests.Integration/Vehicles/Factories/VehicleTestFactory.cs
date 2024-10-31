using VehicleManager.Application.Vehicles.Commands.AddInsurance;
using VehicleManager.Application.Vehicles.Commands.ChangeVehicleInformation;
using VehicleManager.Application.Vehicles.Commands.CreateVehicle;
using VehicleManager.Application.Vehicles.Commands.DeleteVehicle;
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
            double.Round(_faker.Random.Double(1.0, 5.4)),
            _faker.Random.Int(60, 300),
            _faker.PickRandom<FuelType>(),
            _faker.PickRandom<GearboxType>(),
            _faker.PickRandom<VehicleType>()
        );

    public Vehicle CreateVehicle(Guid userId = default)
        => new VehicleBuilder()
            .WithBrand(_faker.Vehicle.Manufacturer())
            .WithModel(_faker.Vehicle.Model())
            .WithYear(_faker.Date.Random.Int(1980, 2024))
            .WithLicensePlate(GenerateLicensePlate())
            .WithVIN(_faker.Vehicle.Vin())
            .WithEngineCapacity(double.Round(_faker.Random.Double(1.0, 5.4)))
            .WithEnginePower(_faker.Random.Int(60, 300))
            .WithFuelType(_faker.PickRandom<FuelType>())
            .WithGearboxType(_faker.PickRandom<GearboxType>())
            .WithVehicleType(_faker.PickRandom<VehicleType>())
            .WithOwner(userId == default ? Guid.NewGuid() : userId)
            .WithServiceBook(ServiceBook.Create())
            .Build();

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
            double.Round(_faker.Random.Double(1.0, 5.4)),
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
            VehicleId = vehicleId == default ? Guid.NewGuid() : vehicleId
        };
}