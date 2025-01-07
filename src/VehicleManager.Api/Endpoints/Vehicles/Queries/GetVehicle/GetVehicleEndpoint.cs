using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Vehicles.Queries.GetVehicle;
using VehicleManager.Application.Vehicles.Queries.GetVehicle.DTO;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Vehicles.Queries.GetVehicle;

internal sealed class GetVehicleEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(VehicleEndpoints.VehicleById, async (
                [FromRoute] Guid vehicleId,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                var response = await mediator.Send(new GetVehicleQuery(vehicleId), cancellationToken);
                return Results.Ok(response);
            })
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows users to get a vehicle by its id"
            })
            .WithTags(VehicleEndpoints.Vehicles)
            .Produces<VehicleDetailsDto>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces<Error>(StatusCodes.Status404NotFound);
    }
}