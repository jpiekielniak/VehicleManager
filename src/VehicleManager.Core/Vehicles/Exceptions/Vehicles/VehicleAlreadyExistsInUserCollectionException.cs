using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Core.Vehicles.Exceptions.Vehicles;

public class VehicleAlreadyExistsInUserCollectionException()
    : VehicleManagerException("Vehicle already exists in user collection");