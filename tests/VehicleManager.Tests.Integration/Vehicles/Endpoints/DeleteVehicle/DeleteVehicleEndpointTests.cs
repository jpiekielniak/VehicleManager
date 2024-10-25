using VehicleManager.Api.Endpoints.Vehicles;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Shared.Middlewares.Exceptions;
using VehicleManager.Tests.Integration.Vehicles.Factories;

namespace VehicleManager.Tests.Integration.Vehicles.Endpoints.DeleteVehicle;

public class DeleteVehicleEndpointTests(VehicleManagerTestFactory factory) : EndpointTests(factory)
{
    [Fact]
    public async Task delete_vehicle_without_authentication_should_return_401_status_code()
    {
        //arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        await SeedDataAsync(user, vehicle);

        //act
        var response =
            await Client.DeleteAsync(VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", vehicle.Id.ToString()));

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task delete_vehicle_with_non_existing_vehicle_should_return_400_status_code()
    {
        //arrange
        var user = _factory.CreateUser();
        await SeedDataAsync(user: user);
        Authorize(user.Id, user.Role.ToString());

        //act
        var response =
            await Client.DeleteAsync(
                VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", Guid.NewGuid().ToString()));

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        await response.Content.ReadFromJsonAsync<Error>().ShouldNotBeNull();
    }

    [Fact]
    public async Task delete_vehicle_with_vehicle_not_belong_to_user_should_return_400_status_code()
    {
        //arrange
        var fakeUser = _factory.CreateUser();
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(fakeUser.Id);
        await SeedDataAsync(fakeUser, vehicle);
        Authorize(user.Id, user.Role.ToString());

        //act
        var response =
            await Client.DeleteAsync(VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", vehicle.Id.ToString()));

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        await response.Content.ReadFromJsonAsync<Error>().ShouldNotBeNull();
    }

    [Fact]
    public async Task delete_vehicle_with_valid_data_should_return_204_status_code()
    {
        //arrange
        var user = _factory.CreateUser();
        var vehicle = _factory.CreateVehicle(user.Id);
        await SeedDataAsync(user, vehicle);
        Authorize(user.Id, user.Role.ToString());

        //act
        var response =
            await Client.DeleteAsync(VehicleEndpoints.VehicleById.Replace("{vehicleId:guid}", vehicle.Id.ToString()));

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    private async Task SeedDataAsync(User? user = default, Vehicle? vehicle = default)
    {
        if (user is not null)
        {
            await DbContext.Users.AddAsync(user);
        }

        if (vehicle is not null)
        {
            await DbContext.Vehicles.AddAsync(vehicle);
        }

        await DbContext.SaveChangesAsync();
    }


    private readonly VehicleTestFactory _factory = new();
}