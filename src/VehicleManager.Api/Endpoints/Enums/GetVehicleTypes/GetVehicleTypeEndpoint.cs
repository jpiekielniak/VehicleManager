using VehicleManager.Core.Vehicles.Entities.Enums;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.EnumHelper;

namespace VehicleManager.Api.Endpoints.Enums.GetVehicleTypes;

internal sealed class GetVehicleTypesEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(EnumEndpoints.VehicleTypes, () =>
            {
                var vehicleTypes = EnumHelper.GetEnumValues<VehicleType>();
                return Task.FromResult(Results.Ok(vehicleTypes));
            })
            .WithTags(EnumEndpoints.Enums)
            .Produces<List<KeyValuePair<int, string>>>();
    }
}