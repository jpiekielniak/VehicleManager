using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Core.Vehicles.Exceptions.Vehicles;

public class VehicleNotBelowToUserException()
    : VehicleManagerException("Vehicle is not below to user");