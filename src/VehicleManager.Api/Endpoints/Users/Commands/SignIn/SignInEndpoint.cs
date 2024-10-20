using VehicleManager.Application.Users.Commands.SignIn;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Api.Endpoints.Users.Commands.SignIn;

internal sealed class SignInEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost(UserEndpoints.SignIn, async (
                [FromBody] SignInCommand command,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                var response = await mediator.Send(command, cancellationToken);
                return Results.Ok(response);
            })
            .AllowAnonymous()
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint allows users to sign in"
            })
            .WithTags(UserEndpoints.Users)
            .Produces<SignInResponse>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}