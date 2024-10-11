using CarManagement.Shared.Exceptions;

namespace CarManagement.Shared.Middlewares.Tokens.Exceptions;

public class TokenExpiredException() : CarManagementException("Token has expired");