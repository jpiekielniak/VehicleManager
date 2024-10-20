using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Core.Users.Exceptions.Users;

public class InvalidPasswordException() : VehicleManagerException("Invalid password");