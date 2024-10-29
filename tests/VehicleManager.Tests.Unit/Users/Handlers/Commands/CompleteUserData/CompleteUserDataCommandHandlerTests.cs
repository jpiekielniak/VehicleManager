using VehicleManager.Application.Users.Commands.CompleteUserData;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Shared.Auth.Context;
using VehicleManager.Tests.Unit.Users.Factories;

namespace VehicleManager.Tests.Unit.Users.Handlers.Commands.CompleteUserData;

public class CompleteUserDataCommandHandlerTests
{
    private async Task Act(CompleteUserDataCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_non_existing_user_id_should_throw_user_not_found_exception()
    {
        // Arrange
        var command = _factory.CreateCompleteUserDataCommand();

        _userRepository.GetAsync(command.UserId, Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
        await _userRepository.Received(1).GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _userRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_user_id_not_matching_context_id_should_throw_action_not_allowed_exception()
    {
        // Arrange
        var command = _factory.CreateCompleteUserDataCommand();

        _userRepository.GetAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(_factory.CreateUser());

        _context.Id.Returns(Guid.NewGuid());

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ActionNotAllowedException>();
        await _userRepository.Received(1).GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _userRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_valid_data_should_update_user_data()
    {
        // Arrange
        var command = _factory.CreateCompleteUserDataCommand();

        var user = _factory.CreateUser();

        _userRepository.GetAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);

        _context.Id.Returns(user.Id);

        // Act
        await Act(command);

        // Assert
        await _userRepository.Received(1).GetAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _userRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private readonly IContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IRequestHandler<CompleteUserDataCommand> _handler;
    private readonly UserTestFactory _factory = new();

    public CompleteUserDataCommandHandlerTests()
    {
        _context = Substitute.For<IContext>();
        _userRepository = Substitute.For<IUserRepository>();

        _handler = new CompleteUserDataCommandHandler(
            _context,
            _userRepository
        );
    }
}