using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Vehicles.Exceptions.ServiceBooks;

public class ServiceBookNotFoundException(Guid serviceBookId)
    : CarManagementException($"ServiceBook with id {serviceBookId} not found");