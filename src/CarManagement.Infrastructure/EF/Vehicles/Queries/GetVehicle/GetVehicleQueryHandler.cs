using CarManagement.Application.Vehicles.Queries.GetVehicle;
using CarManagement.Application.Vehicles.Queries.GetVehicle.DTO;
using CarManagement.Core.Users.Exceptions.Users;
using CarManagement.Core.Users.Repositories;
using CarManagement.Core.Vehicles.Exceptions;
using CarManagement.Core.Vehicles.Exceptions.Vehicles;
using CarManagement.Core.Vehicles.Repositories;
using CarManagement.Shared.Auth.Context;

namespace CarManagement.Infrastructure.EF.Vehicles.Queries.GetVehicle;

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