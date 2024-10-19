using CarManagement.Application.Vehicles.Commands.CreateVehicle;
using CarManagement.Application.Vehicles.Commands.DeleteVehicle;
using CarManagement.Core.Vehicles.Builders;
using CarManagement.Core.Vehicles.Entities;
using CarManagement.Core.Vehicles.Entities.Enums;

namespace CarManagement.Tests.Unit.Vehicles.Factories;

public class VehicleTestFactory
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