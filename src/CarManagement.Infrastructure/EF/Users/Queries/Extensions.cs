using CarManagement.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;
using CarManagement.Core.Users.Entities;

namespace CarManagement.Infrastructure.EF.Users.Queries;

public static class Extensions
{
    internal static UserDetailsDto AsDetailsDto(this User user)
        => new(
            user.Id,
            user.Email,
            user.Username,
            user.PhoneNumber,
            user.CreatedAt
        );
}