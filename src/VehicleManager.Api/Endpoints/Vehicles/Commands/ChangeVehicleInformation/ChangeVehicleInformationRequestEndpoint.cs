using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Api.Endpoints.Vehicles.Commands.ChangeVehicleInformation;

internal sealed class ChangeVehicleInformationRequestEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPut(VehicleEndpoints.VehicleById, async (
                [AsParameters] ChangeVehicleInformationRequest request,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = request.Command with { VehicleId = request.VehicleId };
                await mediator.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to change the information of a vehicle"
            })
            .WithTags(VehicleEndpoints.Vehicles)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}