using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Tests.Unit.Vehicles.Entities.Builders.TestData.Invalid;
using VehicleManager.Tests.Unit.Vehicles.Entities.Builders.TestData.Valid;

namespace VehicleManager.Tests.Unit.Vehicles.Entities.Builders;

public class VehicleBuilderTests
{
    [Theory]
    [ClassData(typeof(VehicleBuilderValidTestData))]
    public void given_valid_data_to_builder_should_build_vehicle_success(
        VehicleBuilderParams vehicleBuilderParams
    )
    {
        //act
        var vehicle = Act(vehicleBuilderParams);

        //assert
        vehicle.ShouldNotBeNull();
        vehicle.Id.ShouldNotBeSameAs(Guid.Empty);
        vehicle.ServiceBook.ShouldNotBeNull();
    }

    [Theory]
    [ClassData(typeof(VehicleBuilderInvalidTestData))]
    public void given_invalid_data_to_builder_should_build_vehicle_fail(
        VehicleBuilderParams vehicleBuilderParams
    )
    {
        //act
        var exception = Record.Exception(() => Act(vehicleBuilderParams));

        //assert
        exception.ShouldNotBeNull();
        exception.ShouldBeAssignableTo<ArgumentException>();
    }

    private static Vehicle Act(VehicleBuilderParams vehicleBuilderParams)
        => new VehicleBuilder()
            .WithBrand(vehicleBuilderParams.Brand)
            .WithModel(vehicleBuilderParams.Model)
            .WithYear(vehicleBuilderParams.Year)
            .WithEngineCapacity(vehicleBuilderParams.EngineCapacity)
            .WithEnginePower(vehicleBuilderParams.EnginePower)
            .WithLicensePlate(vehicleBuilderParams.LicensePlate)
            .WithVIN(vehicleBuilderParams.VIN)
            .WithVehicleType(vehicleBuilderParams.VehicleType)
            .WithFuelType(vehicleBuilderParams.FuelType)
            .WithGearboxType(vehicleBuilderParams.GearboxType)
            .WithOwner(Guid.NewGuid())
            .WithServiceBook(Core.Vehicles.Entities.ServiceBook.Create())
            .Build();
}