using VehicleManager.Application.Admin.Queries.BrowseUsers.DTO;
using VehicleManager.Core.Common.Helpers;
using VehicleManager.Core.Users.Entities;

namespace VehicleManager.Infrastructure.EF.Admin.Queries;

public static class AdminQueryExtensions
{
    public static UserDto AsDto(this User user)
        => new(
            user.Id,
            user.Email,
            user.Role.GetDisplay(),
            user.CreatedAt
        );
}