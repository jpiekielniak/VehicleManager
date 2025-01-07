using VehicleManager.Application.Common.Interfaces.Context;
using VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails;
using VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Infrastructure.EF.Users.Queries.GetCurrentLoggedUserDetails;

internal sealed class GetCurrentLoggedUserDetailsQueryHandler(
    IContext context,
    IUserRepository userRepository
) : IRequestHandler<GetCurrentLoggedUserDetailsQuery, UserDetailsDto>
{
    public async Task<UserDetailsDto> Handle(GetCurrentLoggedUserDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var userId = context.Id;
        var user = await userRepository.GetAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(userId);
        }

        return user.AsDetailsDto();
    }
}