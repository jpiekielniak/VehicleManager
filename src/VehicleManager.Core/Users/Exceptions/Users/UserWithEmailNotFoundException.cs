using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Core.Users.Exceptions.Users;

public class UserWithEmailNotFoundException(string email) 
    : VehicleManagerException($"User with email: {email} not found");