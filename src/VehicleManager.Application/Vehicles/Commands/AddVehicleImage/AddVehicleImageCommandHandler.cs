using VehicleManager.Application.Integrations.BlobStorage.Services;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Application.Vehicles.Commands.AddVehicleImage;

internal sealed class AddVehicleImageCommandHandler(
    IBlobStorageService blobStorageService,
    IVehicleRepository vehicleRepository,
    IImageRepository imageRepository
) : IRequestHandler<AddVehicleImageCommand, AddVehicleImageResponse>
{
    public async Task<AddVehicleImageResponse> Handle(AddVehicleImageCommand command,
        CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetAsync(command.VehicleId, cancellationToken)
                      ?? throw new VehicleNotFoundException(command.VehicleId);

        if (vehicle.Image is not null)
        {
            await blobStorageService.DeleteImageAsync(vehicle.Image.BlobUrl, cancellationToken);
        }

        var blobUrl = await blobStorageService.UploadImageAsync(command.Image, cancellationToken);

        var image = Image.Create(
            vehicle.Id,
            blobUrl,
            command.Image.FileName
        );

        await imageRepository.AddAsync(image, cancellationToken);
        await imageRepository.SaveChangesAsync(cancellationToken);

        return new AddVehicleImageResponse(image.Id);
    }
}