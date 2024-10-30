using VehicleManager.Application.Vehicles.Commands.AddInsurance;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Api.Endpoints.Vehicles.Commands.AddInsurance;

internal sealed class AddInsuranceRequestEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(VehicleEndpoints.Insurances, async (
                [AsParameters] AddInsuranceRequest request,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = request.Command with { VehicleId = request.VehicleId };
                var response = await mediator.Send(command, cancellationToken);
                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithTags(VehicleEndpoints.Vehicles)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to add insurance to a vehicle",
            })
            .Produces<AddInsuranceResponse>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}