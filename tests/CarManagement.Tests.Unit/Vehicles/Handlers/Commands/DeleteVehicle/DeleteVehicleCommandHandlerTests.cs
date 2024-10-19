using CarManagement.Application.Vehicles.Commands.DeleteVehicle;
using CarManagement.Core.Vehicles.Exceptions;
using CarManagement.Core.Vehicles.Repositories;
using CarManagement.Shared.Auth.Context;
using CarManagement.Tests.Unit.Vehicles.Factories;

namespace CarManagement.Tests.Unit.Vehicles.Handlers.Commands.DeleteVehicle;

public class DeleteVehicleCommandHandlerTests
{
    [Fact]
    public async Task given_valid_vehicle_id_should_delete_vehicle()
    {
        // Arrange
        var vehicle = _factory.CreateVehicle();
        var command = new DeleteVehicleCommand(vehicle.Id);

        _vehicleRepository.GetAsync(vehicle.Id, Arg.Any<CancellationToken>()).Returns(vehicle);
        _context.Id.Returns(vehicle.UserId);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _vehicleRepository.Received(1).DeleteAsync(vehicle, Arg.Any<CancellationToken>());
        await _vehicleRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_invalid_vehicle_id_should_throw_vehicle_not_found_exception()
    {
        // Arrange
        var command = new DeleteVehicleCommand(Guid.NewGuid());

        _vehicleRepository.GetAsync(command.VehicleId, Arg.Any<CancellationToken>()).ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<VehicleNotFoundException>();
    }

    [Fact]
    public async Task given_vehicle_not_belong_to_user_should_throw_vehicle_not_belong_to_user_exception()
    {
        // Arrange
        var vehicle = _factory.CreateVehicle();
        var command = new DeleteVehicleCommand(vehicle.Id);

        _vehicleRepository.GetAsync(vehicle.Id, Arg.Any<CancellationToken>()).Returns(vehicle);
        _context.Id.Returns(Guid.NewGuid());

        // Act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<VehicleNotBelowToUserException>();
    }

    private readonly IRequestHandler<DeleteVehicleCommand> _handler;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IContext _context;
    private readonly VehicleTestFactory _factory = new();

    public DeleteVehicleCommandHandlerTests()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();
        _context = Substitute.For<IContext>();

        _handler = new DeleteVehicleCommandHandler(
            _context,
            _vehicleRepository
        );
    }
}