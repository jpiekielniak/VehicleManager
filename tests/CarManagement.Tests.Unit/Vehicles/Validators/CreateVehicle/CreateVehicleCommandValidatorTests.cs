using CarManagement.Application.Vehicles.Commands.CreateVehicle;
using CarManagement.Core.Vehicles.Repositories;
using CarManagement.Tests.Unit.Vehicles.Factories;

namespace CarManagement.Tests.Unit.Vehicles.Validators.CreateVehicle;

public class CreateVehicleCommandValidatorTests
{
    [Fact]
    public async Task validate_create_vehicle_command_with_valid_data_should_return_no_errors()
    {
        //arrange
        var command = _factory.CreateVehicleCommand();
        _vehicleRepository.ExistsWithVinAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(true);
        _vehicleRepository.ExistsWithLicensePlateAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(true);

        //act
        var result = await _validator.TestValidateAsync(command);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    private readonly IVehicleRepository _vehicleRepository;
    private readonly IValidator<CreateVehicleCommand> _validator;
    private readonly VehicleTestFactory _factory = new();

    public CreateVehicleCommandValidatorTests()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();
        _validator = new CreateVehicleCommandValidator(_vehicleRepository);
    }
}