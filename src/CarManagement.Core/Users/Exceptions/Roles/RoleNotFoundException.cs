using CarManagement.Shared.Exceptions;

namespace CarManagement.Core.Users.Exceptions.Roles;

public class RoleNotFoundException(string role) : CarManagementException($"Role {role} not found");