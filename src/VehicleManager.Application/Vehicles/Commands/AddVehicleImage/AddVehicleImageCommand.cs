using Microsoft.AspNetCore.Http;

namespace VehicleManager.Application.Vehicles.Commands.AddVehicleImage;

internal record AddVehicleImageCommand(Guid VehicleId, IFormFile Image) : IRequest<AddVehicleImageResponse>;