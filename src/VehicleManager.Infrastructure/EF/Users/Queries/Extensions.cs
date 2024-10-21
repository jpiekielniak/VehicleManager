using VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Infrastructure.EF.Users.Queries;

public static class Extensions
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