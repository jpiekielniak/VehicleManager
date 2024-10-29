using VehicleManager.Api.Endpoints.ServiceBooks;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Tests.Integration.ServiceBooks.Factories;

namespace VehicleManager.Tests.Integration.ServiceBooks.Endpoints.BrowseServices;

public class BrowseServicesEndpointTest : ServiceBookTest
{
    [Fact]
    public async Task browse_services_without_authentication_should_return_401_status_code()
    {
        // Act
        var response =
            await Client.GetAsync(
                ServiceBookEndpoints.Services.Replace("{serviceBookId:guid}", Guid.NewGuid().ToString()));

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task browse_services_with_invalid_service_book_id_should_return_400_status_code()
    {
        //Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());

        // Act
        var response =
            await Client.GetAsync(
                ServiceBookEndpoints.Services.Replace("{serviceBookId:guid}", Guid.NewGuid().ToString()));

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task browse_services_with_valid_service_book_id_should_return_200_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        var services = _factory.CreateServices(vehicle.ServiceBook, 5);
        Authorize(user.Id, user.Role.ToString());
        await SeedDataAsync(user, vehicle, services: services);

        // Act
        var response =
            await Client.GetAsync(
                ServiceBookEndpoints.Services.Replace("{serviceBookId:guid}", vehicle.ServiceBookId.ToString()));

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldNotBeNull();
    }

    private readonly ServiceBookTestFactory _factory = new();
}