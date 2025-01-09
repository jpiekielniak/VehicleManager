using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Application.Users.Commands.SignUp.Events;

namespace VehicleManager.Infrastructure.Common.Emails.Notifications.SendWelcomeEmailNotification;

internal sealed class SendWelcomeEmailNotificationHandler(IEmailService emailService)
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