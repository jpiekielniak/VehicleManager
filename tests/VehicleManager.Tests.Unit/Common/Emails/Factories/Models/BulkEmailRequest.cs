namespace VehicleManager.Tests.Unit.Common.Emails.Factories.Models;

public record BulkEmailRequest(
    string Title,
    string Content,
    IEnumerable<string> Emails
);