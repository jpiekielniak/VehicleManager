using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Application.Users.Commands.SignUp.Notifications;

namespace VehicleManager.Infrastructure.Common.Emails.Notifications.UserSignedUp;

internal sealed class UserSignedUpNotificationHandler(IEmailService emailService)
    : INotificationHandler<UserSignedUpNotification>
{
    public async Task Handle(UserSignedUpNotification notification, CancellationToken cancellationToken)
    {
        await emailService.SendWelcomeEmailNotificationAsync(
            notification.Email,
            cancellationToken
        );
    }
}