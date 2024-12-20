using VehicleManager.Application.Vehicles.Commands.AddInsurance;
using VehicleManager.Application.Vehicles.Commands.ChangeVehicleInformation;
using VehicleManager.Application.Vehicles.Commands.CreateVehicle;
using VehicleManager.Application.Vehicles.Commands.DeleteInsurance;
using VehicleManager.Application.Vehicles.Commands.DeleteVehicle;
using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Entities.Enums;

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
            .WithOwner(userId == default ? Guid.NewGuid() : userId)
            .Build();

    public DeleteVehicleCommand DeleteVehicleCommand() => new(Guid.NewGuid());

    private string GenerateLicensePlate()
        => _faker.Random.String2(3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
           + _faker.Random.String2(5, "0123456789");

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

    public DeleteInsuranceCommand CreateDeleteInsuranceCommand(Guid vehicleId = default, Guid insuranceId = default)
        => new(
            vehicleId == default ? Guid.NewGuid() : vehicleId,
            insuranceId == default ? Guid.NewGuid() : insuranceId
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
            vehicleId == default ? Guid.NewGuid() : vehicleId,
            insuranceId == default ? Guid.NewGuid() : insuranceId
        );
}