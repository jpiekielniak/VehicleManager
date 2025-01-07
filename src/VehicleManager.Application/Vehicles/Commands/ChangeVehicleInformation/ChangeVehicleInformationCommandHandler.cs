using VehicleManager.Application.Common.Interfaces.Context;
using VehicleManager.Core.Users.Exceptions.Users;
using VehicleManager.Core.Users.Repositories;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Exceptions.Vehicles;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Application.Vehicles.Commands.ChangeVehicleInformation;

internal sealed class ChangeVehicleInformationCommandHandler(
    IVehicleRepository vehicleRepository,
    IContext context,
    IUserRepository userRepository
) : IRequestHandler<ChangeVehicleInformationCommand>
{
    public async Task Handle(ChangeVehicleInformationCommand command,
        CancellationToken cancellationToken)
    {
        var currentLoggedUserId = context.Id;

        var user = await userRepository.AnyAsync(u => u.Id == currentLoggedUserId, cancellationToken);

        if (!user)
        {
            throw new UserNotFoundException(currentLoggedUserId);
        }

        var vehicle = await vehicleRepository.GetAsync(command.VehicleId, cancellationToken);

        if (vehicle is null)
        {
            throw new VehicleNotFoundException(command.VehicleId);
        }

        if (vehicle.UserId != currentLoggedUserId)
        {
            throw new VehicleNotBelowToUserException();
        }

        new VehicleBuilder(vehicle)
            .WithBrand(command.Brand)
            .WithModel(command.Model)
            .WithYear(command.Year)
            .WithLicensePlate(command.LicensePlate)
            .WithVIN(command.Vin)
            .WithEngineCapacity(command.EngineCapacity)
            .WithEnginePower(command.EnginePower)
            .WithFuelType(command.FuelType)
            .WithGearboxType(command.GearboxType)
            .WithVehicleType(command.VehicleType)
            .Build();

        await vehicleRepository.SaveChangesAsync(cancellationToken);
    }
}