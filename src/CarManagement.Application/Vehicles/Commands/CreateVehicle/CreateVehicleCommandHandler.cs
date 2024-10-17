using CarManagement.Core.Vehicles.Builders;
using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Exceptions;
using CarManagement.Core.Vehicles.Repositories;
using CarManagement.Shared.Auth;
using CarManagement.Shared.Auth.Context;

namespace CarManagement.Application.Vehicles.Commands.CreateVehicle;

internal sealed class CreateVehicleCommandHandler(
    IVehicleRepository vehicleRepository,
    IContext context
) : IRequestHandler<CreateVehicleCommand, CreateVehicleResponse>
{
    public async Task<CreateVehicleResponse> Handle(CreateVehicleCommand command,
        CancellationToken cancellationToken)
    {
        if (context.Id != command.UserId)
        {
            throw new AccessForbiddenException();
        }

        var vehicleExists = await vehicleRepository.ExistsAsync(command.Vin, command.UserId, cancellationToken);

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
            .WithOwner(command.UserId)
            .WithServiceBook(ServiceBook.Create())
            .Build();

        await vehicleRepository.AddAsync(vehicle, cancellationToken);
        await vehicleRepository.SaveChangesAsync(cancellationToken);

        return new CreateVehicleResponse(vehicle.Id);
    }
}