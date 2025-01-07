using VehicleManager.Application.Common.Interfaces.Context;
using VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails;
using VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Infrastructure.EF.Users.Queries.GetCurrentLoggedUserDetails;
using VehicleManager.Tests.Unit.Users.Factories;

namespace VehicleManager.Tests.Unit.Users.Handlers.Queries.GetCurrentLoggedUserDetails;

public class GetCurrentLoggedUserDetailsQueryHandlerTests
{
    private async Task<UserDetailsDto> Act(GetCurrentLoggedUserDetailsQuery query)
        => await _handler.Handle(query, CancellationToken.None);

    [Fact]
    public async Task given_invalid_query_should_throw_user_not_found_exception()
    {
        // Arrange
        var query = new GetCurrentLoggedUserDetailsQuery();
        _context.Id.Returns(Guid.NewGuid());
        _userRepository
            .GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(query));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
        _context.Received(1);
        await _userRepository.Received(1).GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }


    [Fact]
    public async Task given_valid_query_should_return_current_logged_user_details()
    {
        // Arrange
        var query = new GetCurrentLoggedUserDetailsQuery();
        var user = _factory.CreateUser();
        _context.Id.Returns(user.Id);
        _userRepository
            .GetAsync(user.Id, Arg.Any<CancellationToken>())
            .Returns(user);

        // Act
        var result = await Act(query);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(user.Id);
        _context.Received(1);
        await _userRepository.Received(1).GetAsync(user.Id, Arg.Any<CancellationToken>());
    }


    private readonly IUserRepository _userRepository;
    private readonly IContext _context;
    private readonly IRequestHandler<GetCurrentLoggedUserDetailsQuery, UserDetailsDto> _handler;
    private readonly UserTestFactory _factory = new();

    public GetCurrentLoggedUserDetailsQueryHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _context = Substitute.For<IContext>();

        _handler = new GetCurrentLoggedUserDetailsQueryHandler(
            _context,
            _userRepository
        );
    }
}