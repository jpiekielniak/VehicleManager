using VehicleManager.Application.Vehicles.Commands.AddVehicleImage;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Validators.AddVehicleImage;

public class AddVehicleImageCommandValidator
{
    [Fact]
    public async Task validate_add_vehicle_image_command_with_valid_data_should_return_no_errors()
    {
        //arrange
        var command = _factory.CreateAddVehicleImageCommand();

        //act
        var result = await _validator.TestValidateAsync(command);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public async Task validate_add_vehicle_image_command_with_invalid_vehicleId_should_return_error()
    {
        //arrange
        var command = _factory.CreateAddVehicleImageCommandWithInvalidVehicleId();

        //act
        var result = await _validator.TestValidateAsync(command);

        //assert
        result.ShouldNotHaveValidationErrorFor(x => x.VehicleId);
    }
    
    [Fact]
    public async Task validate_add_vehicle_image_command_with_invalid_image_should_return_error()
    {
        //arrange
        var command = _factory.CreateAddVehicleImageCommandWithInvalidImage();

        //act
        var result = await _validator.TestValidateAsync(command);

        //assert
        result.ShouldNotHaveValidationErrorFor(x => x.Image);
    }
    
    private readonly IValidator<AddVehicleImageCommand> _validator = new InlineValidator<AddVehicleImageCommand>();
    private readonly VehicleTestFactory _factory = new();
}