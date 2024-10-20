using VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;
using VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles.DTO;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;
using VehicleManager.Shared.Pagination;

namespace VehicleManager.Api.Endpoints.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;

internal sealed class BrowseCurrentLoggedUserVehiclesEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(VehicleEndpoints.BasePath, async (
                [AsParameters] SieveModel sieveModel,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new BrowseCurrentLoggedUserVehiclesQuery(sieveModel);
                var result = await mediator.Send(query, cancellationToken);
                return result;
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows you to browse the vehicles of the current logged user."
            })
            .WithTags(VehicleEndpoints.Vehicles)
            .Produces<PaginationResult<VehicleDto>>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status401Unauthorized);
    }
}