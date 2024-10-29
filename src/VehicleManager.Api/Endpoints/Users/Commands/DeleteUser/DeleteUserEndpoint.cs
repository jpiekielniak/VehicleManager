using VehicleManager.Application.Users.Commands.DeleteUser;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Api.Endpoints.Users.Commands.DeleteUser;

internal sealed class DeleteUserEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete(UserEndpoints.UserById, async (
                [FromRoute(Name = "userId")] Guid userId,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteUserCommand(userId);
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