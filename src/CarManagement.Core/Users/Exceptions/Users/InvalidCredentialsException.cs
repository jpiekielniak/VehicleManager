using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Users.Exceptions.Users;

public class InvalidCredentialsException() : CarManagementException("Invalid credentials");