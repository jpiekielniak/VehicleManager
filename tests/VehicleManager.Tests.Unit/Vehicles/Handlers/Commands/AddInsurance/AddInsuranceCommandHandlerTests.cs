using VehicleManager.Application.Vehicles.Commands.AddInsurance;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Handlers.Commands.AddInsurance;

public class AddInsuranceCommandHandlerTests
{
    private async Task<AddInsuranceResponse> Act(AddInsuranceCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_invalid_vehicle_id_should_throw_vehicle_not_found_exception()
    {
        // Arrange
        var command = _factory.CreateAddInsuranceCommand();
        _vehicleRepository.GetAsync(command.VehicleId, Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<VehicleNotFoundException>();
        await _vehicleRepository.Received(1).GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>(), Arg.Any<bool>());
        await _vehicleRepository.DidNotReceive().UpdateAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_valid_vehicle_id_should_add_insurance()
    {
        // Arrange
        var vehicle = _factory.CreateVehicle();
        var command = _factory.CreateAddInsuranceCommand(vehicle.Id);
        _vehicleRepository.GetAsync(command.VehicleId, Arg.Any<CancellationToken>())
            .Returns(vehicle);

        // Act
        var response = await Act(command);

        // Assert
        response.ShouldNotBeNull();
        await _vehicleRepository.Received(1).GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>(), Arg.Any<bool>());
        await _vehicleRepository.Received(1).UpdateAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>());
    }

    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRequestHandler<AddInsuranceCommand, AddInsuranceResponse> _handler;
    private readonly VehicleTestFactory _factory = new();

    public AddInsuranceCommandHandlerTests()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();

        _handler = new AddInsuranceCommandHandler(
            _vehicleRepository
        );
    }
}