using VehicleManager.Core.Vehicles.Entities.Enums;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.EnumHelper;

namespace VehicleManager.Api.Endpoints.Enums.GetGearboxTypes;

internal sealed class GetGearboxTypesEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(EnumEndpoints.GearboxTypes, () =>
            {
                var gearboxTypes = EnumHelper.GetEnumValues<GearboxType>();
                return Task.FromResult(Results.Ok(gearboxTypes));
            })
            .WithTags(EnumEndpoints.Enums)
            .Produces<List<KeyValuePair<int, string>>>();
    }
}