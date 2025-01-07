using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Core.Users.Exceptions.Users;

public class UserNotFoundException(Guid userId)
    : VehicleManagerException($"User with Id: {userId} not found");