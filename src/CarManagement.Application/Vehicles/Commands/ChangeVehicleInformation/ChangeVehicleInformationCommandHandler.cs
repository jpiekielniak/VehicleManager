using CarManagement.Core.Users.Exceptions.Users;
using CarManagement.Core.Users.Repositories;
using CarManagement.Core.Vehicles.Builders;
using CarManagement.Core.Vehicles.Exceptions.Vehicles;
using CarManagement.Core.Vehicles.Repositories;
using CarManagement.Shared.Auth.Context;

namespace CarManagement.Application.Vehicles.Commands.ChangeVehicleInformation;

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

        vehicle = new VehicleBuilder(vehicle)
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

        await vehicleRepository.UpdateAsync(vehicle, cancellationToken);
        await vehicleRepository.SaveChangesAsync(cancellationToken);
    }
}