using VehicleManager.Application.Integrations.BlobStorage.Services;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Application.Vehicles.Commands.DeleteVehicleImage;

internal sealed class DeleteVehicleImageCommandHandler(
    IVehicleRepository vehicleRepository,
    IBlobStorageService blobStorageService
) : IRequestHandler<DeleteVehicleImageCommand>
{
    public async Task Handle(DeleteVehicleImageCommand command, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetAsync(command.VehicleId, cancellationToken)
                      ?? throw new VehicleNotFoundException(command.VehicleId);

        if (vehicle.Image is null)
        {
            throw new VehicleDoesNotHaveImageException(command.VehicleId);
        }

        await blobStorageService.DeleteImageAsync(vehicle.Image.BlobUrl, cancellationToken);

        vehicle.RemoveImage();

        await vehicleRepository.UpdateAsync(vehicle, cancellationToken);
        await vehicleRepository.SaveChangesAsync(cancellationToken);
    }
}