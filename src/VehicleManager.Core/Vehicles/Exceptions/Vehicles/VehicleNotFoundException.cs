using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Core.Vehicles.Exceptions.Vehicles;

public class VehicleNotFoundException(Guid vehicleId)
    : VehicleManagerException($"Vehicle with id {vehicleId} was not found");