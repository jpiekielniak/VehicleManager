namespace VehicleManager.Application.ServiceBooks.Queries.GetInspection.DTO;

public record InspectionDetailsDto(
    Guid Id,
    string Title,
    DateTimeOffset ScheduledDate,
    DateTimeOffset? PerformDate,
    string InspectionType
);