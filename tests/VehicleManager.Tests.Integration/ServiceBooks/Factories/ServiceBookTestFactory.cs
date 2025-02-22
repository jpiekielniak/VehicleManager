using VehicleManager.Application.ServiceBooks.Commands.AddInspection;
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
            ServiceBookId = serviceBookId ?? FastGuid.NewGuid()
        };

    public Vehicle CreateVehicle(Guid userId = default)
        => new VehicleBuilder()
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
            .WithOwner(userId == Guid.Empty ? FastGuid.NewGuid() : userId)
            .WithServiceBook(ServiceBook.Create())
            .Build();

    public User CreateUser()
        => new UserBuilder()
            .WithEmail(_faker.Internet.Email())
            .WithPassword(_faker.Internet.Password())
            .WithRole(Role.User)
            .Build();

    public AddInspectionCommand CreateAddInspectionCommand(Guid? serviceBookId = default)
        => new(
            _faker.Lorem.Word(),
            _faker.Date.Future(),
            _faker.Date.Future(),
            _faker.PickRandom<InspectionType>()
        )
        {
            ServiceBookId = serviceBookId ?? FastGuid.NewGuid()
        };

    public Inspection CreateInspection(ServiceBook serviceBook)
        => new InspectionBuilder()
            .WithTitle(_faker.Lorem.Word())
            .WithScheduledDate(_faker.Date.Future())
            .WithPerformDate(_faker.Date.Future())
            .WithServiceBook(serviceBook)
            .Build();

    public Service CreateService(ServiceBook serviceBook)
        => new ServiceBuilder()
            .WithTitle(_faker.Lorem.Word())
            .WithDescription(_faker.Lorem.Sentence())
            .WithServiceDate(_faker.Date.Past())
            .WithServiceBook(serviceBook)
            .Build();

    public List<Service> CreateServices(ServiceBook serviceBook, int numberOfServices)
    {
        var services = new List<Service>();

        for (var i = 0; i < numberOfServices; i++)
        {
            services.Add(CreateService(serviceBook));
        }

        return services;
    }

    private string GenerateLicensePlate()
        => _faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
           + _faker.Random.String2(5, "0123456789");
}