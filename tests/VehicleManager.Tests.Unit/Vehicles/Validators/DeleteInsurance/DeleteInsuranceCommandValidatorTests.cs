using VehicleManager.Application.Vehicles.Commands.DeleteInsurance;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Validators.DeleteInsurance;

public class DeleteInsuranceCommandValidatorTests
{
    private const string InsuranceIdRequired = "InsuranceId is required";
    private const string VehicleIdRequired = "VehicleId is required";

    [Fact]
    public async Task given_valid_command_when_validating_then_should_not_have_errors()
    {
        // Arrange
        var command = _factory.CreateDeleteInsuranceCommand();

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task given_empty_vehicle_id_when_validating_then_should_have_validation_error()
    {
        // Arrange
        var command = _factory.CreateDeleteInsuranceCommand(Guid.Empty, Guid.NewGuid());

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.VehicleId)
            .WithErrorMessage(VehicleIdRequired);
    }

    [Fact]
    public async Task given_empty_insurance_id_when_validating_then_should_have_validation_error()
    {
        // Arrange
        var command = _factory.CreateDeleteInsuranceCommand(Guid.NewGuid(), Guid.Empty);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.InsuranceId)
            .WithErrorMessage(InsuranceIdRequired);
    }

    [Fact]
    public async Task given_both_ids_empty_when_validating_then_should_have_multiple_validation_errors()
    {
        // Arrange
        var command = _factory.CreateDeleteInsuranceCommand(Guid.Empty, Guid.Empty);

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.VehicleId);
        result.ShouldHaveValidationErrorFor(x => x.InsuranceId);
    }
    
    private readonly DeleteInsuranceCommandValidator _validator = new();
    private readonly VehicleTestFactory _factory = new();
}