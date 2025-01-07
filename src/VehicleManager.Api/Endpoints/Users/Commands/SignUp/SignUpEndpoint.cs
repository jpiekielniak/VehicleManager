using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Users.Commands.SignUp;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Users.Commands.SignUp;

internal sealed class SignUpEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(UserEndpoints.SignUp, async (
                [FromBody] SignUpCommand command,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                await mediator.Send(command, cancellationToken);
                return Results.Created();
            })
            .AllowAnonymous()
            .WithOpenApi(option => new OpenApiOperation(option)
            {
                Summary = "This endpoint allows users to sign up"
            })
            .WithTags(UserEndpoints.Users)
            .Produces(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}