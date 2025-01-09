using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Users.Commands.ResetPassword;

internal sealed class ResetPasswordEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(UserEndpoints.ResetPassword, async (
                [AsParameters] ResetPasswordEndpointRequest request,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                var command = request.Command with { Token = request.Token };
                await mediator.Send(command, cancellationToken);
            })
            .WithTags(UserEndpoints.Users)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to reset the password of a user.",
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}