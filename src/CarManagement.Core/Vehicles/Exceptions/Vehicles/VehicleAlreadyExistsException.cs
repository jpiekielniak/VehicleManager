using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Vehicles.Exceptions.Vehicles;

public class VehicleAlreadyExistsException(string vin)
    : CarManagementException($"Vehicle with VIN {vin} already exists");