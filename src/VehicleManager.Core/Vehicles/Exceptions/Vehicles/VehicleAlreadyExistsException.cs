using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Core.Vehicles.Exceptions.Vehicles;

public class VehicleAlreadyExistsException(string vin)
    : VehicleManagerException($"Vehicle with VIN {vin} already exists");