namespace CarManagement.Shared.Endpoints;

public interface IEndpointDefinition
{
    void DefineEndpoint(IEndpointRouteBuilder endpoint);
}