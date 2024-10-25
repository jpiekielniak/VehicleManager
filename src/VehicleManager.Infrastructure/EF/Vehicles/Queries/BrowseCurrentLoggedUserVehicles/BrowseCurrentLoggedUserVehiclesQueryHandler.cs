using VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Shared.Auth.Context;
using VehicleManager.Shared.Pagination;
using VehicleDto = VehicleManager.Application.Vehicles.Queries.BrowseCurrentLoggedUserVehicles.DTO.VehicleDto;

namespace VehicleManager.Infrastructure.EF.Vehicles.Queries.BrowseCurrentLoggedUserVehicles;

internal sealed class BrowseCurrentLoggedUserVehiclesQueryHandler(
    IContext context,
    IUserRepository userRepository,
    IVehicleRepository vehicleRepository,
    ISieveProcessor sieveProcessor
) : IRequestHandler<BrowseCurrentLoggedUserVehiclesQuery, PaginationResult<VehicleDto>>
{
    public async Task<PaginationResult<VehicleDto>> Handle(
        BrowseCurrentLoggedUserVehiclesQuery query,
        CancellationToken cancellationToken)
    {
        var currentLoggedUserId = context.Id;
        var user = await userRepository.AnyAsync(u => u.Id == currentLoggedUserId, cancellationToken);

        if (!user)
        {
            throw new UserNotFoundException(currentLoggedUserId);
        }

        var vehicles = await vehicleRepository.GetVehiclesByUserId(currentLoggedUserId, cancellationToken);

        query.SieveModel.Sorts ??= "CreatedAt";

        var sieveResult = await sieveProcessor
            .Apply(query.SieveModel, vehicles)
            .Select(v => new VehicleDto(v.Id, v.Brand, v.Model, v.LicensePlate))
            .ToListAsync(cancellationToken);

        var totalCount = sieveResult.Count;
        
        var result = new PaginationResult<VehicleDto>(
            sieveResult,
            totalCount,
            query.SieveModel.PageSize ?? 5,
            query.SieveModel.Page ?? 1
        );

        return result;
    }
}