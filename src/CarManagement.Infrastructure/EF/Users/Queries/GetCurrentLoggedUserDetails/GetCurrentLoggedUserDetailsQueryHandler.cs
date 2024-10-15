using CarManagement.Application.Users.Queries.GetCurrentLoggedUserDetails;
using CarManagement.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;
using CarManagement.Core.Users.Exceptions.Users;
using CarManagement.Core.Users.Repositories;
using CarManagement.Shared.Auth.Context;

namespace CarManagement.Infrastructure.EF.Users.Queries.GetCurrentLoggedUserDetails;

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