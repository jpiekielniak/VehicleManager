using VehicleManager.Application.Vehicles.Commands.DeleteInsurance;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Handlers.Commands.DeleteInsurance;

public class DeleteInsuranceCommandHandlerTests
{
    private async Task Act(DeleteInsuranceCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_invalid_vehicle_id_should_throw_vehicle_not_found_exception()
    {
        //Arrange
        var command = _factory.CreateDeleteInsuranceCommand();

        _vehicleRepository.GetAsync(command.VehicleId, Arg.Any<CancellationToken>())
            .ReturnsNull();

        //Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<VehicleNotFoundException>();
    }

    [Fact]
    public async Task given_invalid_insurance_id_should_throw_insurance_not_found_exception()
    {
        //Arrange
        var command = _factory.CreateDeleteInsuranceCommand();
        var vehicle = _factory.CreateVehicle();

        _vehicleRepository.GetAsync(command.VehicleId, Arg.Any<CancellationToken>())
            .Returns(vehicle);

        //Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InsuranceNotFoundException>();
    }

    [Fact]
    public async Task given_valid_command_should_remove_insurance()
    {
        //Arrange
        var vehicle = _factory.CreateVehicle();
        var insurance = _factory.CreateInsurance(vehicle);
        var command = _factory.CreateDeleteInsuranceCommand(vehicle.Id, insurance.Id);
        vehicle.AddInsurance(insurance);

        _vehicleRepository.GetAsync(command.VehicleId, Arg.Any<CancellationToken>())
            .Returns(vehicle);

        //Act
        await Act(command);

        //Assert
        vehicle.Insurances.ShouldNotContain(insurance);
    }

    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRequestHandler<DeleteInsuranceCommand> _handler;
    private readonly VehicleTestFactory _factory = new();

    public DeleteInsuranceCommandHandlerTests()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();

        _handler = new DeleteInsuranceCommandHandler(
            _vehicleRepository
        );
    }
}