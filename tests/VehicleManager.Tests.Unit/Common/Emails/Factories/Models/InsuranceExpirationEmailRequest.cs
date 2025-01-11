namespace VehicleManager.Tests.Unit.Common.Emails.Factories;

public record InsuranceExpirationEmailRequest(
    string Email,
    string VehicleInfo,
    DateTimeOffset ExpirationDate,
    string PolicyNumber,
    string Provider
);