using VehicleManager.Application.Common.Interfaces.Context;
using VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;
using VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles.DTO;
using VehicleManager.Core.Common.Pagination;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Infrastructure.EF.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;

namespace VehicleManager.Tests.Unit.Vehicles.Handlers.Queries.BrowseCurrentLoggedUserVehicles;

public class BrowseCurrentLoggedUserVehiclesQueryHandlerTests
{
    private async Task<PaginationResult<VehicleDto>> Act(BrowseCurrentLoggedUserVehiclesQuery query)
        => await _handler.Handle(query, CancellationToken.None);

    [Fact]
    public async Task given_invalid_user_id_should_throw_user_not_found_exception()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _context.Id.Returns(userId);
        _userRepository
            .AnyAsync(u => u.Id == userId, Arg.Any<CancellationToken>())
            .Returns(true);

        var sieveModel = new SieveModel();
        var query = new BrowseCurrentLoggedUserVehiclesQuery(sieveModel);

        // Act
        var exception = await Record.ExceptionAsync(() => Act(query));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
    }

    private readonly IContext _context;
    private readonly IUserRepository _userRepository;
    private readonly BrowseCurrentLoggedUserVehiclesQueryHandler _handler;

    public BrowseCurrentLoggedUserVehiclesQueryHandlerTests()
    {
        var vehicleRepository = Substitute.For<IVehicleRepository>();
        var sieveProcessor = Substitute.For<ISieveProcessor>();

        _context = Substitute.For<IContext>();
        _userRepository = Substitute.For<IUserRepository>();

        _handler = new BrowseCurrentLoggedUserVehiclesQueryHandler(
            _context,
            _userRepository,
            vehicleRepository,
            sieveProcessor
        );
    }
}