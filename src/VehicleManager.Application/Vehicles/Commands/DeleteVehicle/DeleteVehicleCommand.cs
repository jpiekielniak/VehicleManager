namespace VehicleManager.Application.Vehicles.Commands.DeleteVehicle;

internal record DeleteVehicleCommand(Guid VehicleId) : IRequest;