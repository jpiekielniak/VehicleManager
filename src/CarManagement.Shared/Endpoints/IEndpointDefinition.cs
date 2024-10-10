namespace CarManagement.Shared.Endpoints;

public interface IEndpointDefinition
{
    void DefineEndpoints(IEndpointRouteBuilder endpoints);
}