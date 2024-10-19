using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Vehicles.Exceptions.Vehicles;

public class VehicleNotBelowToUserException()
    : CarManagementException("Vehicle is not below to user");