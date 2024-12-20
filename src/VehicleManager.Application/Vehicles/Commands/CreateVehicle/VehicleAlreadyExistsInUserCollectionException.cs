using VehicleManager.Shared.Exceptions;

namespace VehicleManager.Application.Vehicles.Commands.CreateVehicle;

internal class VehicleAlreadyExistsInUserCollectionException() : VehicleManagerException("Vehicle already exists in user collection");