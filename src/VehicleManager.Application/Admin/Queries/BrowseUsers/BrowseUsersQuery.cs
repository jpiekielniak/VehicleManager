using VehicleManager.Application.Admin.Queries.BrowseUsers.DTO;
using VehicleManager.Shared.Pagination;

namespace VehicleManager.Application.Admin.Queries.BrowseUsers;

public record BrowseUsersQuery(SieveModel SieveModel) : IRequest<PaginationResult<UserDto>>;