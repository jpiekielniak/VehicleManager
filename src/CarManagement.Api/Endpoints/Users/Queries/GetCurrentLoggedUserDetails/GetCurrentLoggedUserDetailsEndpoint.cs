using CarManagement.Application.Users.Queries.GetCurrentLoggedUserDetails;
using CarManagement.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;
using CarManagement.Shared.Auth.Policies;
using CarManagement.Shared.Endpoints;
using CarManagement.Shared.Middlewares.Exceptions;

namespace CarManagement.Api.Endpoints.Users.Queries.GetCurrentLoggedUserDetails;

internal sealed class GetCurrentLoggedUserDetailsEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(UserEndpoints.BasePath, async (
                [FromServices] IMediator mediator
            ) =>
            {
                var result = await mediator.Send(
                    new GetCurrentLoggedUserDetailsQuery()
                );
                return Results.Ok(result);
            })
            .RequireAuthorization(AuthorizationPolicies.UserPolicy)
            .WithTags(UserEndpoints.Users)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "This endpoint returns the details of the currently logged user"
            })
            .Produces<UserDetailsDto>(StatusCodes.Status200OK)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}