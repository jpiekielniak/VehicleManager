using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Shared.Middlewares.Tokens.Exceptions;

public class TokenExpiredException() : VehicleManagerException("Token has expired");