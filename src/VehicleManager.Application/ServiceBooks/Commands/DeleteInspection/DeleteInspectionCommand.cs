namespace VehicleManager.Application.ServiceBooks.Commands.DeleteInspection;

internal record DeleteInspectionCommand(Guid ServiceBookId, Guid InspectionId) : IRequest;