using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Core.Users.Exceptions.Users;

public class ActionNotAllowedException() : VehicleManagerException("Action is not allowed for this user");