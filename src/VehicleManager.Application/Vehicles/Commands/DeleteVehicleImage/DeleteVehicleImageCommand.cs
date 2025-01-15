namespace VehicleManager.Application.Vehicles.Commands.DeleteVehicleImage;

internal record DeleteVehicleImageCommand(Guid VehicleId) : IRequest;