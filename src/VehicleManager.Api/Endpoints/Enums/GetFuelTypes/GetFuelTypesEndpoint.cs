using VehicleManager.Core.Vehicles.Entities.Enums;
using VehicleManager.Shared.Endpoints;
using VehicleManager.Shared.EnumHelper;

namespace VehicleManager.Api.Endpoints.Enums.GetFuelTypes;

internal sealed class GetFuelTypesEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(EnumEndpoints.FuelTypes, () =>
            {
                var fuelTypes = EnumHelper.GetEnumValues<FuelType>();
                return Task.FromResult(Results.Ok(fuelTypes));
            })
            .WithTags(EnumEndpoints.Enums)
            .Produces<List<KeyValuePair<int, string>>>();
    }
}