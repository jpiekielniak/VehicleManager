using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Application.ServiceBooks.Commands.AddInspection;

internal record AddInspectionCommand(
    string Title,
    DateTimeOffset ScheduledDate,
    DateTimeOffset? PerformDate,
    InspectionType InspectionType) : IRequest<AddInspectionResponse>
{
    internal Guid ServiceBookId { get; init; }
}