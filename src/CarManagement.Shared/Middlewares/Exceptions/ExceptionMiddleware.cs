using CarManagement.Shared.Exceptions;

namespace CarManagement.Shared.Middlewares.Exceptions;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        return exception switch
        {
            CarManagementException ex => GetExceptionResponse(context, ex),
            ValidationException ex => GetExceptionResponse(context, ex),
            _ => GetExceptionResponse(context)
        };
    }


    private static async Task GetExceptionResponse(HttpContext context, CarManagementException exception)
        => await SetResponseAsync(context, HttpStatusCode.BadRequest, exception.Message);

    private static async Task GetExceptionResponse(HttpContext context, ValidationException ex)
    {
        var error = ex.Errors
            .Select(q => new
            {
                message = q.ErrorMessage
            })
            .FirstOrDefault();

        await SetResponseAsync(context, HttpStatusCode.BadRequest, error);
    }

    private static async Task GetExceptionResponse(HttpContext context)
    {
        var message = new
        {
            message = "There was an error."
        };

        await SetResponseAsync(context, HttpStatusCode.InternalServerError, message);
    }

    private static async Task SetResponseAsync(HttpContext context, HttpStatusCode statusCode, object message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var jsonResponse = JsonSerializer.Serialize(message);
        await context.Response.WriteAsync(jsonResponse);
    }
}