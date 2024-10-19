using CarManagement.Application.Vehicles.Commands.DeleteVehicle;
using CarManagement.Shared.Auth.Policies;
using CarManagement.Shared.Endpoints;
using CarManagement.Shared.Middlewares.Exceptions;

namespace CarManagement.Api.Endpoints.Vehicles.Commands.DeleteVehicle;

internal sealed class DeleteVehicleEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete(VehicleEndpoints.VehicleById, async (
                [FromRoute] Guid vehicleId,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                var command = new DeleteVehicleCommand(vehicleId);
                await mediator.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization(AuthorizationPolicies.UserPolicy)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows users to delete a vehicle by its id"
            })
            .WithTags(VehicleEndpoints.Vehicles)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}