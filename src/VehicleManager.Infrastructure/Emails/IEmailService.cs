namespace VehicleManager.Infrastructure.Emails;

public interface IEmailService
{
    Task SendInsuranceExpirationEmailAsync(string email, string vehicleInfo, DateTimeOffset expirationDate, 
        string policyNumber, string provider, CancellationToken cancellationToken);
}