using VehicleManager.Application.Common.Interfaces.Context;
using VehicleManager.Application.Vehicles.Queries.GetVehicle;
using VehicleManager.Application.Vehicles.Queries.GetVehicle.DTO;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Infrastructure.EF.Vehicles.Queries.GetVehicle;
using VehicleManager.Tests.Unit.Vehicles.Factories;

namespace VehicleManager.Tests.Unit.Vehicles.Handlers.Queries.GetVehicle;

public class GetVehicleQueryHandlerTests
{
    private async Task<VehicleDetailsDto> Act(GetVehicleQuery query)
        => await _handler.Handle(query, CancellationToken.None);

    [Fact]
    public async Task given_invalid_user_should_throw_user_not_found_exception()
    {
        // Arrange
        var query = new GetVehicleQuery(Guid.NewGuid());
        _context.Id.Returns(Guid.NewGuid());
        _userRepository
            .AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        var exception = await Record.ExceptionAsync(() => Act(query));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
    }


    [Fact]
    public async Task given_valid_query_should_return_vehicle_details()
    {
        // Arrange
        var vehicle = _factory.CreateVehicle();
        var query = new GetVehicleQuery(vehicle.Id);
        _context.Id.Returns(Guid.NewGuid());
        _userRepository
            .AnyAsync(Arg.Any<Expression<Func<User, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(true);
        _vehicleRepository
            .GetAsync(query.VehicleId, Arg.Any<CancellationToken>())
            .Returns(vehicle);

        // Act
        var result = await Act(query);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(vehicle.Id);
        result.ShouldBeOfType<VehicleDetailsDto>();
    }


    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IContext _context;
    private readonly IRequestHandler<GetVehicleQuery, VehicleDetailsDto> _handler;
    private readonly VehicleTestFactory _factory = new();

    public GetVehicleQueryHandlerTests()
    {
        _vehicleRepository = Substitute.For<IVehicleRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _context = Substitute.For<IContext>();

        _handler = new GetVehicleQueryHandler(
            _vehicleRepository,
            _userRepository,
            _context
        );
    }
}