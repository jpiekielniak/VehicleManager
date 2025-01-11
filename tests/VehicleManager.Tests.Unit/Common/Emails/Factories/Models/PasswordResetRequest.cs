namespace VehicleManager.Tests.Unit.Common.Emails.Factories.Models;

public record PasswordResetRequest(
    string Email,
    string Token
);