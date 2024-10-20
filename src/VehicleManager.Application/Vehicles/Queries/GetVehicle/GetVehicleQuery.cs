using VehicleManager.Application.Vehicles.Queries.GetVehicle.DTO;

namespace VehicleManager.Application.Vehicles.Queries.GetVehicle;

public record GetVehicleQuery(Guid VehicleId) : IRequest<VehicleDetailsDto>;