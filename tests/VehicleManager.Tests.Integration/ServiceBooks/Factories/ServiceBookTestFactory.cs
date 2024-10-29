using VehicleManager.Application.ServiceBooks.Commands.AddService;
using VehicleManager.Application.ServiceBooks.Commands.AddService.DTO;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Tests.Integration.ServiceBooks.Factories;

internal class ServiceBookTestFactory
{
    private readonly Faker _faker = new();

    public AddServiceCommand CreateAddServiceCommand(Guid? serviceBookId = default)
        => new(
            _faker.Lorem.Word(),
            _faker.Lorem.Sentence(),
            _faker.Date.Past(),
            [
                new CostDto(_faker.Lorem.Word(), _faker.Random.Decimal(1, 1000)),
                new CostDto(_faker.Lorem.Word(), _faker.Random.Decimal(1, 1000))
            ]
        )
        {
            ServiceBookId = serviceBookId ?? Guid.NewGuid()
        };

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
    
    public User CreateUser()
        => new UserBuilder()
            .WithEmail(_faker.Internet.Email())
            .WithPassword(_faker.Internet.Password())
            .WithRole(Role.User)
            .Build();
    
    private string GenerateLicensePlate()
        => _faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
           + _faker.Random.String2(5, "0123456789");
}