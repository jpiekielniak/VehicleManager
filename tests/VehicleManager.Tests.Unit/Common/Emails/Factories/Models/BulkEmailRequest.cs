namespace VehicleManager.Tests.Unit.Common.Emails.Factories;

public record BulkEmailRequest(
    string Title,
    string Content,
    IEnumerable<string> Emails
);