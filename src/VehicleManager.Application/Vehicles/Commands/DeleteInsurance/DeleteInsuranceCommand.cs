namespace VehicleManager.Application.Vehicles.Commands.DeleteInsurance;

internal record DeleteInsuranceCommand(Guid VehicleId, Guid InsuranceId) : IRequest;