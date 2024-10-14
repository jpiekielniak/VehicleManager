using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Users.Exceptions.Users;

public class InvalidPasswordException() : CarManagementException("Invalid password");