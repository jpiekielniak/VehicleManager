using VehicleManager.Shared.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace VehicleManager.Shared.Middlewares.Exceptions;

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
            await HandleExceptionAsync(ex, context);
        }
    }

    private static Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        return exception switch
        {
            VehicleManagerException ex => SetResponseAsync(ex, context),
            ValidationException ex => SetResponseAsync(ex, context),
            _ => SetInternalServerErrorResponse(context)
        };
    }

    private static async Task SetResponseAsync<T>(T ex, HttpContext context)
        where T : Exception
    {
        var errorMessage = ex is ValidationException validationException
            ? validationException.Errors.FirstOrDefault()?.ErrorMessage
            : ex.Message;

        var error = new Error(errorMessage);
        var response = new ErrorsResponse(error);

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        await context.Response.WriteAsJsonAsync(response.Error);
    }

    private static async Task SetInternalServerErrorResponse(HttpContext context)
    {
        var error = new Error(
            "An error occurred while processing your request"
        );

        var response = new ErrorsResponse(error);

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(response.Error);
    }
}