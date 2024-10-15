using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Users.Exceptions.Users;

public class UserNotFoundException(Guid userId)
    : CarManagementException($"User with Id: {userId} not found");