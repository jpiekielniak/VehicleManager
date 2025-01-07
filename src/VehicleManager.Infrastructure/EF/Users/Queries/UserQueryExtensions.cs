using VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;
using VehicleManager.Core.Common.Helpers;
using VehicleManager.Core.Users.Entities;

namespace VehicleManager.Infrastructure.EF.Users.Queries;

public static class UserQueryExtensions
{
    internal static UserDetailsDto AsDetailsDto(this User user)
        => new(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Role.GetDisplay(),
            user.CreatedAt
        );
}