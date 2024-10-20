using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Core.Users.Exceptions.Users;

public class UserAlreadyExistsException(string value)
    : VehicleManagerException($"User with {value} already exists");