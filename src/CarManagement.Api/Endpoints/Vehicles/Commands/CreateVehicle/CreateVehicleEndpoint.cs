using CarManagement.Application.Vehicles.Commands.CreateVehicle;
using CarManagement.Shared.Auth.Policies;
using CarManagement.Shared.Endpoints;
using CarManagement.Shared.Middlewares.Exceptions;

namespace CarManagement.Api.Endpoints.Vehicles.Commands.CreateVehicle;

internal sealed class CreateVehicleEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(VehicleEndpoints.Vehicles, async (
                [FromBody] CreateVehicleCommand command,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                var response = await mediator.Send(command, cancellationToken);
                return Results.Created($"{VehicleEndpoints.Vehicles}/{response.VehicleId}", response);
            })
            .RequireAuthorization(AuthorizationPolicies.UserPolicy)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows users to create a new vehicle"
            })
            .WithTags(VehicleEndpoints.Vehicles)
            .Produces<CreateVehicleResponse>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}