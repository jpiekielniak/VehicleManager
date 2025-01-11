using VehicleManager.Application.Emails.Interfaces;
using VehicleManager.Tests.Unit.Common.Emails.Factories;

namespace VehicleManager.Tests.Unit.Common.Emails;

public class EmailServiceTests
{
    private async Task Act(Func<IEmailService, CancellationToken, Task> action)
        => await action(_emailService, CancellationToken.None);

    [Fact]
    public async Task given_valid_insurance_data_should_send_expiration_email()
    {
        // Arrange
        var request = _factory.CreateInsuranceExpirationEmailRequest();
        _emailService
            .SendInsuranceExpirationEmailAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<DateTimeOffset>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(Task.CompletedTask);

        // Act
        await Act((service, cancellationToken) => service.SendInsuranceExpirationEmailAsync(
            request.Email,
            request.VehicleInfo,
            request.ExpirationDate,
            request.PolicyNumber,
            request.Provider,
            cancellationToken
        ));

        // Assert
        await _emailService.Received(1).SendInsuranceExpirationEmailAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<DateTimeOffset>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task given_multiple_valid_emails_should_send_notification_to_all_users()
    {
        // Arrange
        var request = _factory.CreateBulkEmailRequest();
        _emailService
            .SendEmailToUsersAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<IEnumerable<string>>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(Task.CompletedTask);

        // Act
        await Act((service, cancellationToken) => service.SendEmailToUsersAsync(
            request.Title,
            request.Content,
            request.Emails,
            cancellationToken
        ));

        // Assert
        await _emailService.Received(1).SendEmailToUsersAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<IEnumerable<string>>(),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task given_valid_reset_token_should_send_password_reset_email()
    {
        // Arrange
        var request = _factory.CreatePasswordResetRequest();
        _emailService
            .SendPasswordResetEmailAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(Task.CompletedTask);

        // Act
        await Act((service, cancellationToken) => service.SendPasswordResetEmailAsync(
            request.Email,
            request.Token,
            cancellationToken
        ));

        // Assert
        await _emailService.Received(1).SendPasswordResetEmailAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task given_new_user_email_should_send_welcome_notification()
    {
        // Arrange
        var email = _factory.CreateEmail();
        _emailService
            .SendWelcomeEmailNotificationAsync(
                Arg.Any<string>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(Task.CompletedTask);

        // Act
        await Act((service, cancellationToken) => service.SendWelcomeEmailNotificationAsync(
            email,
            cancellationToken
        ));

        // Assert
        await _emailService.Received(1).SendWelcomeEmailNotificationAsync(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task given_valid_inspection_data_should_send_expiration_email()
    {
        // Arrange
        var request = _factory.CreateInspectionExpirationEmailRequest();
        _emailService
            .SendInspectionExpirationEmailAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<DateTimeOffset?>(),
                Arg.Any<string>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(Task.CompletedTask);

        // Act
        await Act((service, cancellationToken) => service.SendInspectionExpirationEmailAsync(
            request.Email,
            request.VehicleInfo,
            request.ExpirationDate,
            request.InspectionType,
            cancellationToken
        ));

        // Assert
        await _emailService
            .Received(1)
            .SendInspectionExpirationEmailAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<DateTimeOffset?>(),
                Arg.Any<string>(),
                Arg.Any<CancellationToken>()
            );
    }


    [Fact]
    public async Task given_user_email_should_send_account_deletion_notification()
    {
        // Arrange
        var email = _factory.CreateEmail();
        _emailService
            .SendUserDeletedEmailAsync(
                Arg.Any<string>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(Task.CompletedTask);

        // Act
        await Act((service, cancellationToken) => service.SendUserDeletedEmailAsync(
            email,
            cancellationToken
        ));

        // Assert
        await _emailService.Received(1).SendUserDeletedEmailAsync(
            Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    private readonly IEmailService _emailService = Substitute.For<IEmailService>();
    private readonly EmailTestFactory _factory = new();
}