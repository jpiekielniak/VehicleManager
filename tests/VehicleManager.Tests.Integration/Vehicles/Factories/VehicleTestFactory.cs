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

    internal ChangeVehicleInformationCommand ChangeVehicleInformationCommand()
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

    public DeleteVehicleCommand DeleteVehicleCommand() => new(Guid.NewGuid());

    private string GenerateLicensePlate()
        => _faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
           + _faker.Random.String2(5, "0123456789");

    public User CreateUser()
    => new UserBuilder()
        .WithEmail(_faker.Internet.Email())
        .WithPassword(_faker.Internet.Password())
        .WithRole(Role.User)
        .Build();
}