namespace VehicleManager.Tests.Unit.Common.Emails.Factories;

public record InspectionExpirationEmailRequest(
    string Email,
    string VehicleInfo,
    DateTimeOffset ExpirationDate,
    string InspectionType
);