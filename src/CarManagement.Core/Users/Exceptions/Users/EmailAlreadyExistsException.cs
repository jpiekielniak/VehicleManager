using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Users.Exceptions.Users;

public class EmailAlreadyExistsException(string email)
    : CarManagementException($"User with email {email} already exists");