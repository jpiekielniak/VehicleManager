using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Application.Users.Commands.DeleteUser.Notifications;

namespace VehicleManager.Infrastructure.Common.Emails.Notifications.UserDeleted;

internal sealed class UserDeletedNotificationHandler(IEmailService emailService)
    : INotificationHandler<UserDeletedNotification>
{
    public async Task Handle(UserDeletedNotification notification, CancellationToken cancellationToken)
    {
        await emailService.SendUserDeletedEmailAsync(notification.Email, cancellationToken);
    }
}