using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Core.Vehicles.Exceptions.Vehicles;

public class InsuranceNotFoundException(Guid insuranceId)
    : VehicleManagerException($"Insurance with id {insuranceId} was not found");