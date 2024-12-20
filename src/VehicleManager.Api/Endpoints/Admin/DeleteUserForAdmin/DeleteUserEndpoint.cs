using VehicleManager.Application.Admin.Commands.DeleteUserForAdmin;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Api.Endpoints.Admin.DeleteUserForAdmin;

internal sealed class DeleteUserEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete(AdminEndpoints.DeleteUser, async (
                [FromBody] DeleteUserForAdminCommand command,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                await mediator.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization(options =>
                options.RequireRole(Role.Admin.ToString())
            )
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows admin to delete user from system"
            })
            .WithTags(AdminEndpoints.Admin)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden);
    }
}