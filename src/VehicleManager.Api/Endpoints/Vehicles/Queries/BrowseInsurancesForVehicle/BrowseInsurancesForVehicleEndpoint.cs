using VehicleManager.Application.Vehicles.Queries.BrowseInsurancesForVehicle;
using VehicleManager.Application.Vehicles.Queries.BrowseInsurancesForVehicle.DTO;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Pagination;

namespace VehicleManager.Api.Endpoints.Vehicles.Queries.BrowseInsurancesForVehicle;

internal sealed class BrowseInsurancesForVehicleEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(VehicleEndpoints.Insurances, async (
                [FromRoute(Name = "vehicleId")] Guid vehicleId,
                [AsParameters] SieveModel sieveModel,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                var query = new BrowseInsurancesForVehicleQuery(vehicleId, sieveModel);
                var result = await mediator.Send(query, cancellationToken);
                return Results.Ok(result);
            })
            .RequireAuthorization()
            .WithTags(VehicleEndpoints.Vehicles)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows you to browse insurances for a vehicle.",
            })
            .Produces<PaginationResult<InsuranceDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}