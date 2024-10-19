using CarManagement.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;
using CarManagement.Core.Vehicles.Repositories.DTO;
using CarManagement.Shared.Endpoints;
using CarManagement.Shared.Middlewares.Exceptions;
using CarManagement.Shared.Pagination;

namespace CarManagement.Api.Endpoints.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;

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