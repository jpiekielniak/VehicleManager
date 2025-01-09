using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Users.Commands.RequestPasswordReset;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Users.Commands.RequestPasswordReset;

internal sealed class RequestPasswordResetEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(UserEndpoints.ForgotPassword, async (
                [FromBody] RequestPasswordResetCommand command,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                await mediator.Send(command, cancellationToken);
                return Results.Ok();
            })
            .WithTags(UserEndpoints.Users)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to request a password reset.",
            })
            .Produces(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}