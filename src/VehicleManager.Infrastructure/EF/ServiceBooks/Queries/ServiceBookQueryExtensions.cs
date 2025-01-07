using VehicleManager.Application.ServiceBooks.Queries.GetInspection.DTO;
using VehicleManager.Application.ServiceBooks.Queries.GetService.DTO;
using VehicleManager.Core.Common.Helpers;
using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.EF.ServiceBooks.Queries;

public static class ServiceBookQueryExtensions
{
    public static InspectionDetailsDto AsDetailsDto(this Inspection inspection)
        => new(
            inspection.Id,
            inspection.Title,
            inspection.ScheduledDate,
            inspection.PerformDate,
            inspection.InspectionType.GetDisplay()
        );

    public static ServiceDetailsDto AsDetailsDto(this Service service)
        => new(
            service.Id,
            service.Title,
            service.Description,
            service.Date,
            service.Costs.Select(x => x.AsDetailsDto()).ToList()
        );

    public static CostDetailsDto AsDetailsDto(this Cost cost)
        => new(
            cost.Id,
            cost.Title,
            cost.Amount
        );
}