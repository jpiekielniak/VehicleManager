using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Application.ServiceBooks.AddInspection;

internal record AddInspectionCommand(
    DateTimeOffset ScheduledDate,
    DateTimeOffset? PerformDate,
    InspectionType InspectionType) : IRequest<AddInspectionResponse>
{
    internal Guid ServiceBookId { get; init; }
}