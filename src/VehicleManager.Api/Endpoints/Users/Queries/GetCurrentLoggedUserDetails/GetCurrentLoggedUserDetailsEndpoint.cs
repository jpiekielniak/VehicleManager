using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails;
using VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;

namespace VehicleManager.Api.Endpoints.Users.Queries.GetCurrentLoggedUserDetails;

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
            .RequireAuthorization()
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