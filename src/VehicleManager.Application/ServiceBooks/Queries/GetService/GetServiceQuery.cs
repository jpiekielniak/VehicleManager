using VehicleManager.Application.ServiceBooks.Queries.GetService.DTO;

namespace VehicleManager.Application.ServiceBooks.Queries.GetService;

public record GetServiceQuery(Guid ServiceBookId, Guid ServiceId) : IRequest<ServiceDetailsDto>;