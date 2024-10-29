using VehicleManager.Api;
using VehicleManager.Api.Endpoints.Vehicles;
using VehicleManager.Shared.Middlewares.Exceptions;
using VehicleManager.Tests.Integration.Vehicles.Factories;

namespace VehicleManager.Tests.Integration.Vehicles.Endpoints.ChangeVehicleInformation;

public class ChangeVehicleInformationEndpointTests : VehicleEndpointTest
{
    [Fact]
    public async Task put_change_vehicle_information_without_authentication_should_return_401_status_code()
    {
        //arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        var command = _factory.CreateChangeVehicleInformationCommand(user.Id);
        await SeedDataAsync(user, vehicle);

        //act
        var response =
            await Client.PutAsJsonAsync(VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", vehicle.Id.ToString()),
                command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task put_change_vehicle_information_with_non_existing_vehicle_should_return_400_status_code()
    {
        //arrange
        var user = _factory.CreateUser();
        var command = _factory.CreateChangeVehicleInformationCommand(user.Id);
        await SeedDataAsync(user);
        Authorize(user.Id, user.Role.ToString());

        //act
        var response =
            await Client.PutAsJsonAsync(
                VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", Guid.NewGuid().ToString()), command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        await response.Content.ReadFromJsonAsync<Error>().ShouldNotBeNull();
    }

    [Fact]
    public async Task put_change_vehicle_information_with_vehicle_not_belong_to_user_should_return_400_status_code()
    {
        //arrange
        var fakeUser = _factory.CreateUser();
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        var command = _factory.CreateChangeVehicleInformationCommand(user.Id);
        await SeedDataAsync(user, vehicle);
        Authorize(fakeUser.Id, fakeUser.Role.ToString());

        //act
        var response =
            await Client.PutAsJsonAsync(VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", vehicle.Id.ToString()),
                command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        await response.Content.ReadFromJsonAsync<Error>().ShouldNotBeNull();
    }

    [Fact]
    public async Task put_change_vehicle_information_with_valid_vehicle_id_should_return_204_status_code()
    {
        // arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        var command = _factory.CreateChangeVehicleInformationCommand(vehicle.Id);
        await SeedDataAsync(user, vehicle);
        Authorize(user.Id, user.Role.ToString());

        //act
        var response =
            await Client.PutAsJsonAsync(VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", vehicle.Id.ToString()),
                command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }


    private readonly VehicleTestFactory _factory = new();
}