using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Users.Commands.DeleteUser;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Users.Commands.DeleteUser;

internal sealed class DeleteUserEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete(UserEndpoints.BasePath, async (
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteUserCommand();
                await mediator.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithTags(UserEndpoints.Users)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows you to delete a user"
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}