using VehicleManager.Application.ServiceBooks.Queries.BrowseInspections.DTO;
using VehicleManager.Application.ServiceBooks.Queries.BrowseServices;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;
using VehicleManager.Shared.Pagination;

namespace VehicleManager.Api.Endpoints.ServiceBooks.Queries.BrowseServices;

internal sealed class BrowseServicesEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(ServiceBookEndpoints.Services, async (
                [FromRoute(Name = "serviceBookId")] Guid serviceBookId,
                [AsParameters] SieveModel sieveModel,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new BrowseServicesQuery(serviceBookId, sieveModel);
                return await mediator.Send(query, cancellationToken);
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to browse services for a specific service book"
            })
            .WithTags(ServiceBookEndpoints.ServiceBooks)
            .Produces<PaginationResult<InspectionDto>>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}