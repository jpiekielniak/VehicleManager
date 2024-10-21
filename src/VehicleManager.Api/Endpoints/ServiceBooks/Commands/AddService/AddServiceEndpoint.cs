using VehicleManager.Application.ServiceBooks.Commands.AddService;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Api.Endpoints.ServiceBooks.Commands.AddService;

internal sealed class AddServiceEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(ServiceBookEndpoints.Services, async (
                    [AsParameters] AddServiceRequest request,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken) =>
                {
                    var command = request.Command with { ServiceBookId = request.ServiceBookId };
                    var response = await mediator.Send(command, cancellationToken);
                    return Results.Created(
                        $"{ServiceBookEndpoints.BasePath}/{command.ServiceBookId}/services/{response.ServiceId}",
                        response);
                }
            ).RequireAuthorization()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint is used to add a new service to a service book",
            })
            .WithTags(ServiceBookEndpoints.ServiceBooks)
            .Produces<AddServiceResponse>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}