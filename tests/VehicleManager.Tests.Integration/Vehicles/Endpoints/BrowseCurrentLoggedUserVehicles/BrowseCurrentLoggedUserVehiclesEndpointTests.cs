using VehicleManager.Api.Endpoints.Vehicles;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Tests.Integration.Vehicles.Factories;

namespace VehicleManager.Tests.Integration.Vehicles.Endpoints.BrowseCurrentLoggedUserVehicles;

public class BrowseCurrentLoggedUserVehiclesEndpointTests : VehicleEndpointTest
{
    [Fact]
    public async Task browse_current_logged_user_vehicles_with_non_existing_user_should_return_400_status_code()
    {
        // Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());

        // Act
        var response = await Client.GetAsync(VehicleEndpoints.Vehicles);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task browse_current_logged_user_vehicles_with_valid_data_should_return_200_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        var vehicles = _factory.CreateVehicles(user.Id, 5);
        const string pageSize = "5";
        await SeedDataAsync(user, vehicles: vehicles);
        Authorize(user.Id, user.Role.ToString());

        // Act
        var response = await Client.GetAsync($"{VehicleEndpoints.BasePath}?PageSize={pageSize}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldNotBeNull();
    }

    private readonly VehicleTestFactory _factory = new();
}