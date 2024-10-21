using VehicleManager.Application.ServiceBooks.Queries.BrowseInspections;
using VehicleManager.Application.ServiceBooks.Queries.BrowseInspections.DTO;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;
using VehicleManager.Shared.Pagination;

namespace VehicleManager.Api.Endpoints.ServiceBooks.Queries.BrowseInspections;

internal sealed class BrowseInspectionsEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(ServiceBookEndpoints.Inspections, async (
                [FromRoute(Name = "serviceBookId")] Guid serviceBookId,
                [AsParameters] SieveModel sieveModel,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new BrowseInspectionQuery(serviceBookId, sieveModel);
                return await mediator.Send(query, cancellationToken);
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to browse inspections for a specific service book"
            })
            .Produces<PaginationResult<InspectionDto>>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}