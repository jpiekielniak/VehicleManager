using VehicleManager.Application.Admin.Commands.DeleteUserForAdmin;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Tests.Unit.Users.Factories;

namespace VehicleManager.Tests.Unit.Admin.Commands.DeleteUserForAdmin;

public class DeleteUserForAdminCommandHandlerTests
{
    private async Task Act(DeleteUserForAdminCommand command)
        => await _handler.Handle(command, CancellationToken.None);
    
    [Fact]
    public async Task given_non_existing_user_should_throw_user_not_found_exception()
    {
        // Arrange
        var command = _factory.CreateDeleteUserForAdminCommand();
        _userRepository
            .GetAsync(command.UserId, Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var exception = await Record.ExceptionAsync(() => Act(command));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserNotFoundException>();
        await _userRepository
            .DidNotReceive()
            .DeleteAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        await _userRepository
            .DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task given_valid_userId_should_delete_user_success()
    {
        // Arrange
        var command = _factory.CreateDeleteUserForAdminCommand();
        var user = _factory.CreateUser();
        _userRepository
            .GetAsync(command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);
        
        // Act
        await Act(command);
        
        // Assert
        await _userRepository
            .Received(1)
            .GetAsync(command.UserId, Arg.Any<CancellationToken>());
        await _userRepository
            .Received(1)
            .DeleteAsync(user, Arg.Any<CancellationToken>());
        await _userRepository
            .Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());

    }
    
    private readonly IRequestHandler<DeleteUserForAdminCommand> _handler;
    private readonly IUserRepository _userRepository;
    private readonly UserTestFactory _factory = new();

    public DeleteUserForAdminCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _handler = new DeleteUserForAdminCommandHandler(_userRepository);
    }
}