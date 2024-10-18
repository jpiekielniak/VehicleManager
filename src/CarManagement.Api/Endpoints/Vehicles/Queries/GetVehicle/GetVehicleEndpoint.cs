using CarManagement.Application.Vehicles.Queries.GetVehicle;
using CarManagement.Application.Vehicles.Queries.GetVehicle.DTO;
using CarManagement.Shared.Endpoints;
using CarManagement.Shared.Middlewares.Exceptions;

namespace CarManagement.Api.Endpoints.Vehicles.Queries.GetVehicle;

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