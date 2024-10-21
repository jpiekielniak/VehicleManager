using VehicleManager.Application.ServiceBooks.Queries.GetService;
using VehicleManager.Application.ServiceBooks.Queries.GetService.DTO;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Api.Endpoints.ServiceBooks.Queries.GetService;

internal sealed class GetServiceRequestEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(ServiceBookEndpoints.Service, async (
                [AsParameters] GetServiceRequest request,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetServiceQuery(request.ServiceBookId, request.ServiceId);
                return await mediator.Send(query, cancellationToken);
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint used to get the details of a service"
            })
            .WithTags(ServiceBookEndpoints.ServiceBooks)
            .Produces<ServiceDetailsDto>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}