using VehicleManager.Application.Vehicles.Commands.AddInsurance;
using VehicleManager.Application.Vehicles.Commands.AddVehicleImage;
using VehicleManager.Application.Vehicles.Commands.ChangeVehicleInformation;
using VehicleManager.Application.Vehicles.Commands.CreateVehicle;
using VehicleManager.Application.Vehicles.Commands.DeleteInsurance;
using VehicleManager.Application.Vehicles.Commands.DeleteVehicle;
using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Entities.Enums;
using VehicleManager.Tests.Unit.Vehicles.Helpers;

namespace VehicleManager.Tests.Unit.Vehicles.Factories;

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

    internal ChangeVehicleInformationCommand ChangeVehicleInformationCommand()
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
            _faker.PickRandom<VehicleType>()
        );

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
            .WithServiceBook(Core.Vehicles.Entities.ServiceBook.Create())
            .WithOwner(userId == Guid.Empty ? Guid.NewGuid() : userId)
            .Build();


    public DeleteVehicleCommand DeleteVehicleCommand() => new(Guid.NewGuid());

    private string GenerateLicensePlate()
        => _faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
           + _faker.Random.String2(5, "0123456789");

    public AddInsuranceCommand CreateAddInsuranceCommand(Guid? vehicleId = null)
        => new(
            _faker.Lorem.Sentence(),
            _faker.Company.CompanyName(),
            _faker.Random.String2(10, "0123456789"),
            _faker.Date.Recent(),
            _faker.Date.Future())
        {
            VehicleId = vehicleId ?? Guid.NewGuid()
        };

    public DeleteInsuranceCommand CreateDeleteInsuranceCommand(Guid? vehicleId = null, Guid? insuranceId = null)
        => new(
            vehicleId ?? Guid.NewGuid(),
            insuranceId ?? Guid.NewGuid()
        );

    public Insurance CreateInsurance(Vehicle vehicle)
        => new InsuranceBuilder()
            .WithTitle(_faker.Lorem.Sentence())
            .WithProvider(_faker.Company.CompanyName())
            .WithPolicyNumber(_faker.Random.String2(10, "0123456789"))
            .WithValidFrom(_faker.Date.Recent())
            .WithValidTo(_faker.Date.Future())
            .WithVehicle(vehicle)
            .Build();

    public GetInsuranceDetailsForVehicleQuery CreateGetInsuranceDetailsForVehicleQuery(Guid vehicleId = default,
        Guid insuranceId = default)
        => new(
            vehicleId == Guid.Empty ? Guid.NewGuid() : vehicleId,
            insuranceId == Guid.Empty ? Guid.NewGuid() : insuranceId
        );

    public Image CreateImage(Guid vehicleId = default)
        => Image.Create(
            vehicleId == Guid.Empty ? Guid.NewGuid() : vehicleId,
            _faker.Image.PlaceholderUrl(100, 100),
            _faker.Lorem.Sentence()
        );

    public AddVehicleImageCommand CreateAddVehicleImageCommand(Guid vehicleId)
        => new(
            vehicleId == Guid.Empty ? Guid.NewGuid() : vehicleId,
            FormFileHelper.FormFileFaker().Generate()
        );

    public AddVehicleImageCommand CreateAddVehicleImageCommand()
        => new(Guid.NewGuid(), FormFileHelper.FormFileFaker().Generate());

    public AddVehicleImageCommand CreateAddVehicleImageCommandWithInvalidVehicleId()
        => new(Guid.Empty, FormFileHelper.FormFileFaker().Generate());

    public AddVehicleImageCommand CreateAddVehicleImageCommandWithInvalidImage()
        => new(Guid.NewGuid(), null);

    public ChangeVehicleInformationCommand CreateChangeVehicleInformationCommand()
        => new(
            _faker.Vehicle.Manufacturer(),
            _faker.Vehicle.Model(),
            _faker.Date.Random.Int(1980, DateTimeOffset.Now.Year),
            GenerateLicensePlate(),
            _faker.Vehicle.Vin(),
            _faker.Random.Int(900, 5400),
            _faker.Random.Int(60, 300),
            _faker.PickRandom<FuelType>(),
            _faker.PickRandom<GearboxType>(),
            _faker.PickRandom<VehicleType>()
        )
        {
            VehicleId = Guid.NewGuid()
        };

    public DeleteVehicleCommand CreateDeleteVehicleCommand(Guid? vehicleId)
        => new(vehicleId ?? Guid.NewGuid());
}