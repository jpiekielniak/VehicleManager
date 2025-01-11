namespace VehicleManager.Tests.Unit.Common.Emails.Factories.Models;

public record InsuranceExpirationEmailRequest(
    string Email,
    string VehicleInfo,
    DateTimeOffset ExpirationDate,
    string PolicyNumber,
    string Provider
);