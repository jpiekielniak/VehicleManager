using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Users.Exceptions.Users;

public class UsernameAlreadyExistsExceptions(string username)
    : CarManagementException($"User with this {username} already exists");