using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Users.Exceptions.Users;

public class UserNotFoundException(string email)
    : CarManagementException($"User with email: {email} not found");