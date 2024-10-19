using CarManagement.Application.Vehicles.Commands.CreateVehicle;
using CarManagement.Tests.Unit.Vehicles.Factories;

namespace CarManagement.Tests.Unit.Vehicles.Validators.CreateVehicle;

public class CreateVehicleCommandValidatorTests
{
    [Fact]
    public async Task validate_create_vehicle_command_with_valid_data_should_return_no_errors()
    {
        //arrange
        var command = _factory.CreateVehicleCommand();

        //act
        var result = await _validator.ValidateAsync(command);

        //assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    private readonly IValidator<CreateVehicleCommand> _validator = new CreateVehicleCommandValidator();
    private readonly VehicleTestFactory _factory = new();

}