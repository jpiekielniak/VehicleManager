using VehicleManager.Api.Common.Endpoints;
using VehicleManager.Core.Common.Helpers;
using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Api.Endpoints.Enums.GetInspectionTypes;

internal sealed class GetInspectionTypesEndpoint : IEndpointDefinition
{
    public void DefineEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet(EnumEndpoints.InspectionTypes, () =>
            {
                var inspectionTypes = EnumHelper.GetEnumValues<InspectionType>();
                return Task.FromResult(Results.Ok(inspectionTypes));
            })
            .WithTags(EnumEndpoints.Enums)
            .Produces<List<KeyValuePair<int, string>>>();
    }
}