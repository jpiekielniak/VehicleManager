using VehicleManager.Api.Endpoints.ServiceBooks;
using VehicleManager.Application.ServiceBooks.Queries.GetService.DTO;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Tests.Integration.ServiceBooks.Factories;

namespace VehicleManager.Tests.Integration.ServiceBooks.Endpoints.GetService;

public class GetServiceEndpointTest : ServiceBookTest
{
    [Fact]
    public async Task get_service_without_authentication_should_return_401_status_code()
    {
        // Arrange
        var url = ServiceBookEndpoints.Service
            .Replace("{serviceBookId:guid}", Guid.NewGuid().ToString())
            .Replace("{serviceId:guid}", Guid.NewGuid().ToString());

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task get_service_with_invalid_service_book_id_should_return_400_status_code()
    {
        // Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());
        var url = ServiceBookEndpoints.Service
            .Replace("{serviceBookId:guid}", Guid.NewGuid().ToString())
            .Replace("{serviceId:guid}", Guid.NewGuid().ToString());

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task get_service_with_invalid_service_id_should_return_400_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        Authorize(user.Id, Role.User.ToString());
        var url = ServiceBookEndpoints.Service
            .Replace("{serviceBookId:guid}", vehicle.ServiceBookId.ToString())
            .Replace("{serviceId:guid}", Guid.NewGuid().ToString());

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task get_service_with_valid_data_should_return_200_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        var service = _factory.CreateService(vehicle.ServiceBook);
        Authorize(user.Id, Role.User.ToString());
        await SeedDataAsync(user, vehicle, service: service);
        var url = ServiceBookEndpoints.Service
            .Replace("{serviceBookId:guid}", vehicle.ServiceBookId.ToString())
            .Replace("{serviceId:guid}", service.Id.ToString());

        // Act
        var response = await Client.GetAsync(url);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ServiceDetailsDto>();
        result.ShouldNotBeNull();
        result.Id.ShouldBe(service.Id);
    }

    private readonly ServiceBookTestFactory _factory = new();
}