namespace CarManagement.Application.Vehicles.Commands.DeleteVehicle;

public record DeleteVehicleCommand(Guid VehicleId) : IRequest;