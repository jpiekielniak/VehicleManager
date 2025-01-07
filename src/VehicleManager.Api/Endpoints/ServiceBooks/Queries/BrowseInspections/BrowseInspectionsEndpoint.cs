using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.ServiceBooks.Queries.BrowseInspections;
using VehicleManager.Application.ServiceBooks.Queries.BrowseInspections.DTO;
using VehicleManager.Core.Common.Pagination;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

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
                var query = new BrowseInspectionsQuery(serviceBookId, sieveModel);
                return await mediator.Send(query, cancellationToken);
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to browse inspections for a specific service book"
            })
            .WithTags(ServiceBookEndpoints.ServiceBooks)
            .Produces<PaginationResult<InspectionDto>>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}