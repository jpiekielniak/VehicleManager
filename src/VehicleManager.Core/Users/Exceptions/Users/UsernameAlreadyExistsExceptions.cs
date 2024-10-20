using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Core.Users.Exceptions.Users;

public class UsernameAlreadyExistsExceptions(string username)
    : VehicleManagerException($"User with this {username} already exists");