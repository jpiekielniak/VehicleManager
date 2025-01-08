using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Application.Admin.Commands.SendEmailToUsers;

internal sealed class SendEmailToUsersCommandHandler(
    IUserRepository userRepository,
    IEmailService emailService
) : IRequestHandler<SendEmailToUsersCommand>
{
    public async Task Handle(SendEmailToUsersCommand command, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetUsersAsync(cancellationToken);
        
        var usersEmails = users
            .Select(u => u.Email)
            .ToList();

        await emailService.SendEmailToUsersAsync(command.Title, command.Content, usersEmails, cancellationToken);
    }
}