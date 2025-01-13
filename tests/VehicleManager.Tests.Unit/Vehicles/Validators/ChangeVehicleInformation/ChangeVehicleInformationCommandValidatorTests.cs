using VehicleManager.Application.Vehicles.Commands.ChangeVehicleInformation;
using VehicleManager.Core.Vehicles.Entities.Enums;
using VehicleManager.Tests.Unit.Vehicles.Factories;
using VehicleManager.Tests.Unit.Vehicles.Helpers;

namespace VehicleManager.Tests.Unit.Vehicles.Validators.ChangeVehicleInformation;

public class ChangeVehicleInformationCommandValidatorTests
{
    [Fact]
    public async Task given_valid_command_when_validating_then_should_not_have_errors()
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand();

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task given_empty_brand_when_validating_then_should_have_required_error(string brand)
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { Brand = brand };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Brand);
    }

    [Fact]
    public async Task given_brand_exceeding_50_characters_when_validating_then_should_have_max_length_error()
    {
        // Arrange
        var brandExceedingMaxLength = new string('x', 51);
        var command = _factory.CreateChangeVehicleInformationCommand() with { Brand = brandExceedingMaxLength };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Brand);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task given_empty_model_when_validating_then_should_have_required_error(string model)
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { Model = model };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Model);
    }

    [Fact]
    public async Task given_model_exceeding_50_characters_when_validating_then_should_have_max_length_error()
    {
        // Arrange
        var modelExceedingMaxLength = new string('x', 51);
        var command = _factory.CreateChangeVehicleInformationCommand() with { Model = modelExceedingMaxLength };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Model);
    }

    [Fact]
    public async Task given_year_below_1900_when_validating_then_should_have_error()
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { Year = 1899 };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Year);
    }

    [Fact]
    public async Task given_future_year_when_validating_then_should_have_error()
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { Year = YearHelper.NextYear };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Year);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task given_empty_license_plate_when_validating_then_should_have_required_error(string licensePlate)
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { LicensePlate = licensePlate };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LicensePlate);
    }

    [Fact]
    public async Task given_license_plate_exceeding_10_characters_when_validating_then_should_have_max_length_error()
    {
        // Arrange
        var licensePlateExceedingMaxLength = new string('x', 11);
        var command = _factory.CreateChangeVehicleInformationCommand() with { LicensePlate = licensePlateExceedingMaxLength };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LicensePlate);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task given_empty_vin_when_validating_then_should_have_required_error(string vin)
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { Vin = vin };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Vin);
    }

    [Fact]
    public async Task given_vin_exceeding_17_characters_when_validating_then_should_have_max_length_error()
    {
        // Arrange
        var vinExceedingMaxLength = new string('x', 18);
        var command = _factory.CreateChangeVehicleInformationCommand() with { Vin = vinExceedingMaxLength };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Vin);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(10001)]
    public async Task given_engine_capacity_out_of_range_when_validating_then_should_have_error(int engineCapacity)
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { EngineCapacity = engineCapacity };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.EngineCapacity);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1001)]
    public async Task given_engine_power_out_of_range_when_validating_then_should_have_error(int enginePower)
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { EnginePower = enginePower };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.EnginePower);
    }

    [Fact]
    public async Task given_invalid_fuel_type_when_validating_then_should_have_error()
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { FuelType = (FuelType)999 };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FuelType);
    }

    [Fact]
    public async Task given_invalid_gearbox_type_when_validating_then_should_have_error()
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { GearboxType = (GearboxType)999 };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.GearboxType);
    }

    [Fact]
    public async Task given_invalid_vehicle_type_when_validating_then_should_have_error()
    {
        // Arrange
        var command = _factory.CreateChangeVehicleInformationCommand() with { VehicleType = (VehicleType)999 };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.VehicleType);
    }

    private readonly IValidator<ChangeVehicleInformationCommand> _validator = new ChangeVehicleInformationCommandValidator();
    private readonly VehicleTestFactory _factory = new();
}