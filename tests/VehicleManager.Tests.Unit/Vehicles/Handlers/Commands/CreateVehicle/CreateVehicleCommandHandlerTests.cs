using VehicleManager.Application.Vehicles.Commands.CreateVehicle;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Shared.Auth.Context;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Handlers.Commands.CreateVehicle;

public class CreateVehicleCommandHandlerTests
{
    [Fact]
    public async Task given_valid_data_should_create_vehicle_success()
    {
        // Arrange
        var command = _factory.CreateVehicleCommand();
        var userId = Guid.NewGuid();
        _vehicleRepository.ExistsAsync(Arg.Any<Expression<Func<Vehicle, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);
        _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(true);
        _context.Id.Returns(userId);

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
        var userId = Guid.NewGuid();
        _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);
        _context.Id.Returns(userId);

        // Act
        var exception = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
        await _vehicleRepository.DidNotReceive().AddAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>());
        await _vehicleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
    
    private readonly IRequestHandler<CreateVehicleCommand, CreateVehicleResponse> _handler;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IContext _context;
    private readonly VehicleTestFactory _factory = new();

    public CreateVehicleCommandHandlerTests()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _context = Substitute.For<IContext>();

        _handler = new CreateVehicleCommandHandler(
            _vehicleRepository,
            _userRepository,
            _context
        );
    }
}