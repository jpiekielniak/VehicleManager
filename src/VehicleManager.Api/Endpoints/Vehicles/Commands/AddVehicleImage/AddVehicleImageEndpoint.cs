using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Vehicles.Commands.AddVehicleImage;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Vehicles.Commands.AddVehicleImage;

internal sealed class AddVehicleImageEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(VehicleEndpoints.VehicleImage, async (
                [FromForm] IFormFile file,
                [FromRoute] Guid vehicleId,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                var command = new AddVehicleImageCommand(vehicleId, file);
                var response = await mediator.Send(command, cancellationToken);
                return Results.Ok(response);
            })
            .RequireAuthorization()
            .DisableAntiforgery()
            .Accepts<IFormFile>("multipart/form-data")
            .WithTags(VehicleEndpoints.Vehicles)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to add an image to a vehicle",
            })
            .Produces<AddVehicleImageResponse>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}