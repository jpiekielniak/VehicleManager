namespace VehicleManager.Shared.Endpoints;

public interface IEndpointDefinition
{
    void DefineEndpoint(IEndpointRouteBuilder endpoint);
}