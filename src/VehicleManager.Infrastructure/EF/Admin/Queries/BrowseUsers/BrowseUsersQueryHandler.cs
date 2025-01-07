using VehicleManager.Application.Admin.Queries.BrowseUsers;
using VehicleManager.Application.Admin.Queries.BrowseUsers.DTO;
using VehicleManager.Core.Common.Pagination;
using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Infrastructure.EF.Admin.Queries.BrowseUsers;

internal sealed class BrowseUsersQueryHandler(
    IUserRepository userRepository,
    ISieveProcessor sieveProcessor
) : IRequestHandler<BrowseUsersQuery, PaginationResult<UserDto>>
{
    public async Task<PaginationResult<UserDto>> Handle(BrowseUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetUsersAsync(cancellationToken);

        query.SieveModel.Sorts ??= "CreatedAt";

        var sortedQuery = sieveProcessor
            .Apply(query.SieveModel, users, applyPagination: false, applySorting: true, applyFiltering: true);

        var totalCount = sortedQuery.Count();

        var skipValue = ((query.SieveModel.Page ?? 1) - 1) * (query.SieveModel.PageSize ?? 5);
        var takeValue = query.SieveModel.PageSize ?? 5;

        var paginatedUsers = await sortedQuery
            .Skip(skipValue)
            .Take(takeValue)
            .Select(u => u.AsDto())
            .ToListAsync(cancellationToken);

        var result = new PaginationResult<UserDto>(
            paginatedUsers,
            totalCount,
            query.SieveModel.PageSize ?? 5,
            query.SieveModel.Page ?? 1
        );

        return result;
    }
}