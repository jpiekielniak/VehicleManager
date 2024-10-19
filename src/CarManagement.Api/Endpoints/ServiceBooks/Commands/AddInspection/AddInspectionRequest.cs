using CarManagement.Application.ServiceBooks.AddInspection;

namespace CarManagement.Api.Endpoints.ServiceBooks.Commands.AddInspection;

internal class AddInspectionRequest
{
    [FromRoute(Name = "serviceBookId")] public Guid ServiceBookId { get; init; }
    [FromBody] public AddInspectionCommand Command { get; init; } = default;
}