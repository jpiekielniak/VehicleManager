using VehicleManager.Application.Admin.Queries.BrowseUsers.DTO;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Shared.Enums;

namespace VehicleManager.Infrastructure.EF.Admin.Queries;

public static class Extensions
{
    public static UserDto AsDto(this User user)
        => new(
            user.Id,
            user.Email,
            user.Role.GetDisplay(),
            user.CreatedAt
        );
}