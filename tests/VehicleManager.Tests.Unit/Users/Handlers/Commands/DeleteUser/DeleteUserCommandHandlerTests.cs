using VehicleManager.Application.Common.Interfaces.Context;
using VehicleManager.Application.Users.Commands.DeleteUser;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Tests.Unit.Users.Factories;

namespace VehicleManager.Tests.Unit.Users.Handlers.Commands.DeleteUser;

public class DeleteUserCommandHandlerTests
{
    private async Task Act(DeleteUserCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_non_existing_user_should_throw_user_not_found_exception()
    {
        // Arrange
        var command = _factory.CreateDeleteUserCommand();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
        await _userRepository
            .DidNotReceive()
            .DeleteAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_different_context_id_should_throw_action_not_allowed_exception()
    {
        // Arrange
        var command = _factory.CreateDeleteUserCommand();
        var user = _factory.CreateUser();
        _userRepository
            .GetAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);
        _context.Id
            .Returns(Guid.NewGuid());

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ActionNotAllowedException>();
        await _userRepository
            .DidNotReceive()
            .DeleteAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_existing_user_should_delete_user()
    {
        // Arrange
        var command = _factory.CreateDeleteUserCommand();
        var user = _factory.CreateUser();
        _userRepository
            .GetAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);
        _context.Id
            .Returns(user.Id);

        // Act
        await Act(command);

        // Assert
        await _userRepository
            .Received(1)
            .DeleteAsync(user, Arg.Any<CancellationToken>());
        await _userRepository
            .Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }


    private readonly IContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IRequestHandler<DeleteUserCommand> _handler;
    private readonly UserTestFactory _factory = new();

    public DeleteUserCommandHandlerTests()
    {
        _context = Substitute.For<IContext>();
        _userRepository = Substitute.For<IUserRepository>();

        _handler = new DeleteUserCommandHandler(
            _context,
            _userRepository
        );
    }
}