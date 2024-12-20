using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;
using VehicleManager.Shared.Auth.Context;

namespace VehicleManager.Application.Vehicles.Commands.CreateVehicle;

internal sealed class CreateVehicleCommandHandler(
    IVehicleRepository vehicleRepository,
    IUserRepository userRepository,
    IContext context
) : IRequestHandler<CreateVehicleCommand, CreateVehicleResponse>
{
    public async Task<CreateVehicleResponse> Handle(CreateVehicleCommand command,
        CancellationToken cancellationToken)
    {
        var currentLoggedInUserId = context.Id;
        var userExists = await userRepository.AnyAsync(u => u.Id == currentLoggedInUserId, cancellationToken);

        if (!userExists)
        {
            throw new UserNotFoundException(currentLoggedInUserId);
        }

        var vehicleExistsInUserCollection = await vehicleRepository.ExistsAsync(
            v => (v.VIN == command.Vin || v.LicensePlate == command.LicensePlate)
                 && v.UserId == currentLoggedInUserId,
            cancellationToken);
        
        if(vehicleExistsInUserCollection)
        {
            throw new VehicleAlreadyExistsInUserCollectionException();
        }

        var vehicle = new VehicleBuilder()
            .WithBrand(command.Brand)
            .WithModel(command.Model)
            .WithYear(command.Year)
            .WithEngineCapacity(command.EngineCapacity)
            .WithEnginePower(command.EnginePower)
            .WithVIN(command.Vin)
            .WithLicensePlate(command.LicensePlate)
            .WithVehicleType(command.VehicleType)
            .WithFuelType(command.FuelType)
            .WithGearboxType(command.GearboxType)
            .WithOwner(currentLoggedInUserId)
            .WithServiceBook(ServiceBook.Create())
            .Build();

        await vehicleRepository.AddAsync(vehicle, cancellationToken);
        await vehicleRepository.SaveChangesAsync(cancellationToken);

        return new CreateVehicleResponse(vehicle.Id);
    }
}