using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Vehicles.Commands.DeleteVehicleImage;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Vehicles.Commands.DeleteVehicleImage;

internal sealed class DeleteVehicleImageEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete(VehicleEndpoints.VehicleImage, async (
                    [FromRoute(Name = "vehicleId")] Guid vehicleId,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken)
                =>
            {
                var command = new DeleteVehicleImageCommand(vehicleId);
                await mediator.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithTags(VehicleEndpoints.Vehicles)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint deletes the image of a vehicle."
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}