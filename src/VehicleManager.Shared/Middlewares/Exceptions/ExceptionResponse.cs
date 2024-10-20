namespace VehicleManager.Shared.Middlewares.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);