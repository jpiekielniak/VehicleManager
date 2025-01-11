namespace VehicleManager.Tests.Unit.Common.Emails.Factories;

public record PasswordResetRequest(
    string Email,
    string Token
);