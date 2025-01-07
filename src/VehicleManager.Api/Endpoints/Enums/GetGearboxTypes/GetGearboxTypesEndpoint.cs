using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Core.Common.Helpers;
using VehicleManager.Core.Vehicles.Entities.Enums;

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