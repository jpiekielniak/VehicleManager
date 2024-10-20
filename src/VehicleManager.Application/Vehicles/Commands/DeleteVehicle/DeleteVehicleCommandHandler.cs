using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Shared.Auth.Context;

namespace VehicleManager.Application.Vehicles.Commands.DeleteVehicle;

internal sealed class DeleteVehicleCommandHandler(
    IContext context,
    IVehicleRepository vehicleRepository
) : IRequestHandler<DeleteVehicleCommand>
{
    public async Task Handle(DeleteVehicleCommand command, CancellationToken cancellationToken)
    {
        var currentLoggedUserId = context.Id;

        var vehicle = await vehicleRepository.GetAsync(command.VehicleId, cancellationToken);
        
        if (vehicle is null)
        {
            throw new VehicleNotFoundException(command.VehicleId);
        }
        
        var isVehicleBelowToUser = vehicle.UserId == currentLoggedUserId;
        
        if (!isVehicleBelowToUser)
        {
            throw new VehicleNotBelowToUserException();
        }
        
        await vehicleRepository.DeleteAsync(vehicle, cancellationToken);
        await vehicleRepository.SaveChangesAsync(cancellationToken);
    }
}