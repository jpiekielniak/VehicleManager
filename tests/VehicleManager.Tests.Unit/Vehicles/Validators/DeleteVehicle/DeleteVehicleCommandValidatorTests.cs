using VehicleManager.Application.Vehicles.Commands.DeleteVehicle;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Validators.DeleteVehicle;

public class DeleteVehicleCommandValidatorTests
{
    private const string VehicleIdIsRequired = "VehicleId is required";

    [Fact]
    public void validate_delete_vehicle_command_with_valid_data_should_return_no_errors()
    {
        // Arrange
        var command = _factory.CreateDeleteVehicleCommand(Guid.NewGuid());

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void validate_delete_vehicle_command_with_empty_vehicle_id_should_return_errors()
    {
        // Arrange
        var command = _factory.CreateDeleteVehicleCommand(Guid.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.ShouldHaveValidationErrorFor(x => x.VehicleId)
            .WithErrorMessage(VehicleIdIsRequired);
    }

    private readonly IValidator<DeleteVehicleCommand> _validator = new DeleteVehicleCommandValidator();
    private readonly VehicleTestFactory _factory = new();
}