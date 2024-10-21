using VehicleManager.Application.ServiceBooks.Queries.GetInspection;
using VehicleManager.Application.ServiceBooks.Queries.GetInspection.DTO;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Api.Endpoints.ServiceBooks.Queries.GetInspection;

internal sealed class GetInspectionRequestEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(ServiceBookEndpoints.Inspection, async (
                    [AsParameters] GetInspectionRequest request,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    var query = new GetInspectionQuery(request.ServiceBookId, request.InspectionId);
                    return await mediator.Send(query, cancellationToken);
                }
            )
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to get the details of a specific inspection"
            })
            .WithTags(ServiceBookEndpoints.ServiceBooks)
            .Produces<InspectionDetailsDto>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}