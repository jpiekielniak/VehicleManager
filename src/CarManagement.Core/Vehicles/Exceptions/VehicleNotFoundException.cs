using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Vehicles.Exceptions;

public class VehicleNotFoundException(Guid vehicleId)
    : CarManagementException($"Vehicle with id {vehicleId} was not found");