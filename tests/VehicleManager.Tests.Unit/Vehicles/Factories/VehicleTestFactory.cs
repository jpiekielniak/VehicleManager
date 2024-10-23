using VehicleManager.Application.Vehicles.Commands.ChangeVehicleInformation;
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

    internal ChangeVehicleInformationCommand ChangeVehicleInformationCommand()
        => new(
            "Peugeot",
            "308 SW",
            2015,
            "KTA94969",
            "VF3**************[VIN]",
            1.6,
            FuelType.Diesel,
            120,
            GearboxType.Manual,
            VehicleType.Car
        );

    public Vehicle CreateVehicle(Guid userId = default)
        => new VehicleBuilder()
            .WithOwner(userId)
            .Build();

    public DeleteVehicleCommand DeleteVehicleCommand()
        => new(Guid.NewGuid());
}