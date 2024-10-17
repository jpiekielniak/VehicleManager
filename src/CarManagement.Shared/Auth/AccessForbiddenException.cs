using CarManagement.Shared.Exceptions;

namespace CarManagement.Shared.Auth;

public class AccessForbiddenException() : CarManagementException("Access forbidden");