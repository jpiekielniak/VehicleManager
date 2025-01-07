using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Vehicles.Commands.CreateVehicle;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Vehicles.Commands.CreateVehicle;

internal sealed class CreateVehicleEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(VehicleEndpoints.BasePath, async (
                [FromBody] CreateVehicleCommand command,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                var response = await mediator.Send(command, cancellationToken);
                return Results.Created($"{VehicleEndpoints.BasePath}/{response.VehicleId}", response);
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows users to create a new vehicle"
            })
            .WithTags(VehicleEndpoints.Vehicles)
            .Produces<CreateVehicleResponse>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}