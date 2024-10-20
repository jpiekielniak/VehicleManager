using VehicleManager.Application.Vehicles.Commands.CreateVehicle;
using VehicleManager.Application.Vehicles.Commands.DeleteVehicle;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Tests.Unit.Vehicles.Factories;

internal class VehicleTestFactory
{
    internal CreateVehicleCommand CreateVehicleCommand()
        => new(
            "brand",
            "model",
            2005,
            "KTA94969",
            "KA12586720KI435",
            2.0,
            138,
            FuelType.Diesel,
            GearboxType.Manual,
            VehicleType.Car
        );

    public Vehicle CreateVehicle()
        => new VehicleBuilder().Build();

    public DeleteVehicleCommand DeleteVehicleCommand()
        => new(Guid.NewGuid());
}