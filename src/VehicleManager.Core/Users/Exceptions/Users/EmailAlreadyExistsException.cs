using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Core.Users.Exceptions.Users;

public class EmailAlreadyExistsException(string email)
    : VehicleManagerException($"User with email {email} already exists");