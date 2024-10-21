using VehicleManager.Application.ServiceBooks.Queries.GetInspection.DTO;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Infrastructure.EF.ServiceBooks.Queries;

public static class Extensions
{
    public static InspectionDetailsDto AsDetailsDto(this Inspection inspection)
        => new(
            inspection.Id,
            inspection.Title,
            inspection.ScheduledDate,
            inspection.PerformDate,
            inspection.InspectionType.GetDisplay()
        );
}