using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Vehicles.Exceptions;

public class VehicleNotBelowToUserException()
    : CarManagementException("Vehicle is not below to user");