using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;

public class InspectionNotFoundException(Guid inspectionId)
    : VehicleManagerException($"Inspection with id {inspectionId} not found");