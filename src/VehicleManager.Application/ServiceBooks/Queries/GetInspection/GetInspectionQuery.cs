using VehicleManager.Application.ServiceBooks.Queries.GetInspection.DTO;

namespace VehicleManager.Application.ServiceBooks.Queries.GetInspection;

public record GetInspectionQuery(Guid ServiceBookId, Guid InspectionId) : IRequest<InspectionDetailsDto>;