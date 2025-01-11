using VehicleManager.Application.Admin.Commands.SendEmailToUsers;
using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Tests.Unit.Admin.Factories;

namespace VehicleManager.Tests.Unit.Admin.Commands.SendEmailToUsers;

public class SendEmailToUsersCommandHandlerTests
{
    private async Task Act(SendEmailToUsersCommand command)
        => await _handler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task given_valid_command_should_send_email_to_users()
    {
        // Arrange
        var command = _factory.CreateSendEmailToUsersCommand();
        var users = _factory.CreateUsers(UsersCount);
        _userRepository
            .GetUsersAsync(Arg.Any<CancellationToken>())
            .Returns(users.AsQueryable());
        _emailService
            .SendEmailToUsersAsync(command.Title, command.Content, Arg.Any<List<string>>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        await Act(command);

        // Assert
        await _userRepository
            .Received()
            .GetUsersAsync(Arg.Any<CancellationToken>());
        await _emailService
            .Received()
            .SendEmailToUsersAsync(command.Title, command.Content, Arg.Any<List<string>>(),
                Arg.Any<CancellationToken>());
    }

    private const int UsersCount = 5;
    private readonly IRequestHandler<SendEmailToUsersCommand> _handler;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly AdminTestFactory _factory = new();

    public SendEmailToUsersCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _emailService = Substitute.For<IEmailService>();

        _handler = new SendEmailToUsersCommandHandler(
            _userRepository,
            _emailService
        );
    }
}