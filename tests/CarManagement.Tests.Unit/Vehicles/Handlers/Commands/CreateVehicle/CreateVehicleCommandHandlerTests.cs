using CarManagement.Application.Vehicles.Commands.CreateVehicle;
using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Exceptions;
using CarManagement.Core.Vehicles.Repositories;
using CarManagement.Shared.Auth;
using CarManagement.Shared.Auth.Context;
using CarManagement.Tests.Unit.Vehicles.Factories;

namespace CarManagement.Tests.Unit.Vehicles.Handlers.Commands.CreateVehicle;

public class CreateVehicleCommandHandlerTests
{
    [Fact]
    public async Task given_valid_data_should_create_vehicle_success()
    {
        // Arrange
        var command = _factory.CreateVehicleCommand();
        _vehicleRepository.ExistsAsync(command.Vin, command.UserId, Arg.Any<CancellationToken>())
            .Returns(false);
        _context.Id.Returns(command.UserId);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.ShouldNotBeNull();
        response.VehicleId.ShouldNotBeSameAs(Guid.Empty);
        response.ShouldBeOfType<CreateVehicleResponse>();
        await _vehicleRepository.Received(1).AddAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>());
        await _vehicleRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_invalid_user_id_should_throw_access_forbidden_exception()
    {
        // Arrange
        var command = _factory.CreateVehicleCommand();
        _context.Id.Returns(Guid.NewGuid());

        // Act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<AccessForbiddenException>();
        await _vehicleRepository.DidNotReceive().AddAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>());
        await _vehicleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_existing_vehicle_should_throw_vehicle_already_exists_exception()
    {
        // Arrange
        var command = _factory.CreateVehicleCommand();
        _vehicleRepository.ExistsAsync(command.Vin, command.UserId, Arg.Any<CancellationToken>())
            .Returns(true);
        _context.Id.Returns(command.UserId);

        // Act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<VehicleAlreadyExistsException>();
        await _vehicleRepository.DidNotReceive().AddAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>());
        await _vehicleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private readonly IRequestHandler<CreateVehicleCommand, CreateVehicleResponse> _handler;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IContext _context;
    private readonly VehicleTestFactory _factory = new();

    public CreateVehicleCommandHandlerTests()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();
        _context = Substitute.For<IContext>();

        _handler = new CreateVehicleCommandHandler(
            _vehicleRepository,
            _context
        );
    }
}