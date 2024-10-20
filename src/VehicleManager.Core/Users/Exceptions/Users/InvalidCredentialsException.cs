using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Core.Users.Exceptions.Users;

public class InvalidCredentialsException() : VehicleManagerException("Invalid credentials");