using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Core.Vehicles.Exceptions.Vehicles;

public class VehicleDoesNotHaveImageException(Guid vehicleId) 
    : VehicleManagerException($"Vehicle with id {vehicleId} does not have an image.");