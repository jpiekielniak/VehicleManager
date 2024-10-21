using VehicleManager.Application.ServiceBooks.Commands.DeleteService;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Api.Endpoints.ServiceBooks.Commands.DeleteService;

internal sealed class DeleteServiceRequestEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete(ServiceBookEndpoints.Service, async (
                [AsParameters] DeleteServiceRequest request,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteServiceCommand(request.ServiceBookId, request.ServiceId);
                await mediator.Send(command, cancellationToken);
            })
            .RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to delete an service from a service book"
            })
            .WithTags(ServiceBookEndpoints.ServiceBooks)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}