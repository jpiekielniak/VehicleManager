using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;

public class ServiceBookNotFoundException(Guid serviceBookId)
    : VehicleManagerException($"ServiceBook with id {serviceBookId} not found");