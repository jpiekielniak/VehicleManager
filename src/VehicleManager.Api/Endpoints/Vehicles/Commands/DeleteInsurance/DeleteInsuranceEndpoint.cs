using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Vehicles.Commands.DeleteInsurance;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Vehicles.Commands.DeleteInsurance;

internal sealed class DeleteInsuranceEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete(VehicleEndpoints.InsuranceById, async (
                [AsParameters] DeleteInsuranceRequest request,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteInsuranceCommand(request.VehicleId, request.InsuranceId);
                await mediator.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithTags(VehicleEndpoints.Vehicles)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to delete insurance from a vehicle",
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}