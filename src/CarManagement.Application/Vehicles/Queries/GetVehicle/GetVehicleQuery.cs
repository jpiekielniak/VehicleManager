using CarManagement.Application.Vehicles.Queries.GetVehicle.DTO;

namespace CarManagement.Application.Vehicles.Queries.GetVehicle;

public record GetVehicleQuery(Guid VehicleId) : IRequest<VehicleDetailsDto>;