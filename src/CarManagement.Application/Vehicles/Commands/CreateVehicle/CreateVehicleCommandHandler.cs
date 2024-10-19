using CarManagement.Core.Users.Exceptions.Users;
using CarManagement.Core.Users.Repositories;
using CarManagement.Core.Vehicles.Builders;
using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Exceptions;
using CarManagement.Core.Vehicles.Repositories;
using CarManagement.Shared.Auth.Context;

namespace CarManagement.Application.Vehicles.Commands.CreateVehicle;

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

        var vehicleExists = await vehicleRepository.ExistsAsync(command.Vin, currentLoggedInUserId, cancellationToken);

        if (vehicleExists)
        {
            throw new VehicleAlreadyExistsException(command.Vin);
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