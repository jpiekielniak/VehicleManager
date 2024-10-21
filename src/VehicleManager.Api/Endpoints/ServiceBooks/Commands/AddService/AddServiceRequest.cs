
using VehicleManager.Application.ServiceBooks.Commands.AddService;

namespace VehicleManager.Api.Endpoints.ServiceBooks.Commands.AddService;

internal class AddServiceRequest
{
    [FromRoute(Name = "serviceBookId")] public Guid ServiceBookId { get; init; }
    [FromBody] public AddServiceCommand Command { get; init; } = default!;
}