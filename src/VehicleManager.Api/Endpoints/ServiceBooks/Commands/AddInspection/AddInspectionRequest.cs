using VehicleManager.Application.ServiceBooks.Commands.AddInspection;

namespace VehicleManager.Api.Endpoints.ServiceBooks.Commands.AddInspection;

internal class AddInspectionRequest
{
    [FromRoute(Name = "serviceBookId")] public Guid ServiceBookId { get; init; }
    [FromBody] public AddInspectionCommand Command { get; init; } = default;
}