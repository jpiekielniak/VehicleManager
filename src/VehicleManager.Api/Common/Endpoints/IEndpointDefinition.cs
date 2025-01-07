namespace VehicleManager.Api.Common.Endpoints;

public interface IEndpointDefinition
{
    void DefineEndpoint(IEndpointRouteBuilder endpoint);
}