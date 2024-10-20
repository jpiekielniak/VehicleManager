using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Core.Users.Exceptions.Roles;

public class RoleNotFoundException(string role) : VehicleManagerException($"Role {role} not found");