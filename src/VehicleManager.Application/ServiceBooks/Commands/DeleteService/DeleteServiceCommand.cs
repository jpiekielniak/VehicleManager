namespace VehicleManager.Application.ServiceBooks.Commands.DeleteService;

internal record DeleteServiceCommand(Guid ServiceBookId, Guid ServiceId) : IRequest;