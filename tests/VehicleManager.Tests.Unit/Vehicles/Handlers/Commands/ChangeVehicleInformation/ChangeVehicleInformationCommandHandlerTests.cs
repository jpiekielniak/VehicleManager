using VehicleManager.Application.Common.Interfaces.Context;
using VehicleManager.Application.Vehicles.Commands.ChangeVehicleInformation;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Handlers.Commands.ChangeVehicleInformation;

public class ChangeVehicleInformationCommandHandlerTests
{
    private async Task Act(ChangeVehicleInformationCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_invalid_user_id_should_throw_user_not_found_exception()
    {
        // Arrange
        var command = _factory.ChangeVehicleInformationCommand();
        _context.Id.Returns(Guid.NewGuid());
        _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
    }

    [Fact]
    public async Task given_invalid_vehicle_id_should_throw_vehicle_not_found_exception()
    {
        // Arrange
        var command = _factory.ChangeVehicleInformationCommand();
        _context.Id.Returns(Guid.NewGuid());
        _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(true);
        _vehicleRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<VehicleNotFoundException>();
    }

    [Fact]
    public async Task given_vehicle_not_belong_to_user_should_throw_vehicle_not_belong_to_user_exception()
    {
        // Arrange
        var command = _factory.ChangeVehicleInformationCommand();
        var vehicle = _factory.CreateVehicle();
        _context.Id.Returns(Guid.NewGuid());
        _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(true);
        _vehicleRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(vehicle);

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<VehicleNotBelowToUserException>();
    }

    [Fact]
    public async Task given_valid_data_should_update_vehicle_information()
    {
        // Arrange
        var command = _factory.ChangeVehicleInformationCommand();
        var userId = Guid.NewGuid();
        var vehicle = _factory.CreateVehicle(userId);
        _context.Id.Returns(userId);
        _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(true);
        _vehicleRepository.GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(vehicle);

        // Act
        await Act(command);

        // Assert
        vehicle.Brand.ShouldBe(command.Brand);
        await _vehicleRepository.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }


    private readonly IVehicleRepository _vehicleRepository;
    private readonly IContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IRequestHandler<ChangeVehicleInformationCommand> _handler;
    private readonly VehicleTestFactory _factory = new();

    public ChangeVehicleInformationCommandHandlerTests()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();
        _context = Substitute.For<IContext>();
        _userRepository = Substitute.For<IUserRepository>();

        _handler = new ChangeVehicleInformationCommandHandler(
            _vehicleRepository,
            _context,
            _userRepository
        );
    }
}