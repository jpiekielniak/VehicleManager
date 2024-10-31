using VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle.DTO;

namespace VehicleManager.Application.Vehicles.Queries.GetInsuranceDetailsForVehicle;

public record GetInsuranceDetailsForVehicleQuery(Guid VehicleId, Guid InsuranceId) : IRequest<InsuranceDetailsDto>;