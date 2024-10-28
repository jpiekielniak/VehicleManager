using VehicleManager.Api.Endpoints.Vehicles;
using VehicleManager.Application.Vehicles.Queries.GetVehicle.DTO;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Shared.Middlewares.Exceptions;
using VehicleManager.Tests.Integration.Vehicles.Factories;

namespace VehicleManager.Tests.Integration.Vehicles.Endpoints.GetVehicle;

public class GetVehicleEndpointTests : EndpointTests
{
    [Fact]
    public async Task get_vehicle_with_non_existing_user_should_return_400_status_code()
    {
        // Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());

        // Act
        var response =
            await Client.GetAsync(VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", Guid.NewGuid().ToString()));

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var content = await response.Content.ReadFromJsonAsync<Error>();
        content.ShouldNotBeNull();
    }

    [Fact]
    public async Task get_vehicle_with_non_existing_vehicle_should_return_400_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        await SeedDataAsync(user);
        Authorize(user.Id, user.Role.ToString());

        // Act
        var response =
            await Client.GetAsync(VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", Guid.NewGuid().ToString()));

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var content = await response.Content.ReadFromJsonAsync<Error>();
        content.ShouldNotBeNull();
    }

    [Fact]
    public async Task get_vehicle_with_valid_data_should_return_200_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        await SeedDataAsync(user, vehicle);
        Authorize(user.Id, user.Role.ToString());

        // Act
        var response =
            await Client.GetAsync(VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", vehicle.Id.ToString()));

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<VehicleDetailsDto>();
        content.ShouldNotBeNull();
    }

    private readonly VehicleTestFactory _factory = new();
}