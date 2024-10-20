using VehicleManager.Application.Vehicles.Queries.GetVehicle;
using VehicleManager.Application.Vehicles.Queries.GetVehicle.DTO;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Shared.Auth.Context;

namespace VehicleManager.Infrastructure.EF.Vehicles.Queries.GetVehicle;

internal sealed class GetVehicleQueryHandler(
    IVehicleRepository vehicleRepository,
    IUserRepository userRepository,
    IContext context
) : IRequestHandler<GetVehicleQuery, VehicleDetailsDto>
{
    public async Task<VehicleDetailsDto> Handle(GetVehicleQuery query,
        CancellationToken cancellationToken)
    {
        var userId = context.Id;

        var userExists = await userRepository.AnyAsync(
            u => u.Id == userId,
            cancellationToken
        );

        if (!userExists)
        {
            throw new UserNotFoundException(userId);
        }

        var vehicle = await vehicleRepository.GetAsync(query.VehicleId, cancellationToken);

        return vehicle?.AsDetailsDto() ?? throw new VehicleNotFoundException(query.VehicleId);
    }
}