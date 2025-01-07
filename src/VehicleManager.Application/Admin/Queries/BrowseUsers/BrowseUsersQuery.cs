using VehicleManager.Application.Admin.Queries.BrowseUsers.DTO;
using VehicleManager.Core.Common.Pagination;

namespace VehicleManager.Application.Admin.Queries.BrowseUsers;

public record BrowseUsersQuery(SieveModel SieveModel) : IRequest<PaginationResult<UserDto>>;