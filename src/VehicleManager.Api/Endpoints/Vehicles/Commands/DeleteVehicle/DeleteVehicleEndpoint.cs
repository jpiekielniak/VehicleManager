using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Vehicles.Commands.DeleteVehicle;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Vehicles.Commands.DeleteVehicle;

internal sealed class DeleteVehicleEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete(VehicleEndpoints.VehicleById, async (
                [FromRoute(Name = "vehicleId")] Guid vehicleId,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                var command = new DeleteVehicleCommand(vehicleId);
                await mediator.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows users to delete a vehicle by its id"
            })
            .WithTags(VehicleEndpoints.Vehicles)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}