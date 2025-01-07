namespace VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);