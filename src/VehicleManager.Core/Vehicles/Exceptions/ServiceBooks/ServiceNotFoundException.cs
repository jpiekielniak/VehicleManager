using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;

public class ServiceNotFoundException(Guid serviceId)
    : VehicleManagerException($"Service with id {serviceId} not found");