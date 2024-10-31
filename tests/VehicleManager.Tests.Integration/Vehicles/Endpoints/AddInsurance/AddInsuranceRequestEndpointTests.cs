using VehicleManager.Api.Endpoints.Vehicles;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Tests.Integration.Vehicles.Factories;

namespace VehicleManager.Tests.Integration.Vehicles.Endpoints.AddInsurance;

public class AddInsuranceRequestEndpointTests : VehicleEndpointTest
{
    [Fact]
    public async Task post_add_insurance_with_non_existing_vehicle_should_return_400_status_code()
    {
        //Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());
        var command = _factory.CreateAddInsuranceCommand();
        var url = VehicleEndpoints.Insurances.Replace("{vehicleId:guid}", Guid.NewGuid().ToString());

        //Act
        var response = await Client.PostAsJsonAsync(url, command);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task post_add_insurance_with_existing_vehicle_should_return_200_status_code()
    {
        //Arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        Authorize(user.Id, user.Role.ToString());
        await SeedDataAsync(user, vehicle);
        var command = _factory.CreateAddInsuranceCommand(vehicle.Id);
        var url = VehicleEndpoints.Insurances.Replace("{vehicleId:guid}", vehicle.Id.ToString());

        //Act
        var response = await Client.PostAsJsonAsync(url, command);

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
    }

    private readonly VehicleTestFactory _factory = new();
}