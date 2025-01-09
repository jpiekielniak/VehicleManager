namespace VehicleManager.Application.Emails.Interfaces;

public interface IEmailService
{
    Task SendInsuranceExpirationEmailAsync(
        string email,
        string vehicleInfo,
        DateTimeOffset expirationDate,
        string policyNumber,
        string provider,
        CancellationToken cancellationToken
    );

    Task SendEmailToUsersAsync(
        string title,
        string content,
        IEnumerable<string> emails,
        CancellationToken cancellationToken
    );

    Task SendPasswordResetEmailAsync(
        string email,
        string token,
        CancellationToken cancellationToken
    );
}