using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Vehicles.Exceptions;

public class VehicleAlreadyExistsException(string vin)
    : CarManagementException($"Vehicle with VIN {vin} already exists");