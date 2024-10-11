using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Users.Exceptions.Users;

public class UserAlreadyExistsException(string value)
    : CarManagementException($"User with {value} already exists");