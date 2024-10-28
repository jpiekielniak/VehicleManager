using VehicleManager.Api.Endpoints.Vehicles;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Shared.Middlewares.Exceptions;
using VehicleManager.Tests.Integration.Vehicles.Factories;

namespace VehicleManager.Tests.Integration.Vehicles.Endpoints.CreateVehicle;

public class CreateVehicleEndpointTests : EndpointTests
{
    [Fact]
    public async Task post_create_vehicle_with_non_existing_user_should_return_400_status_code()
    {
        //arrange
        var command = _factory.CreateVehicleCommand();
        Authorize(Guid.NewGuid(), Role.User.ToString());
        
        //act
        var response = await Client.PostAsJsonAsync(VehicleEndpoints.BasePath, command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        await response.Content.ReadFromJsonAsync<Error>().ShouldNotBeNull();
    }

    [Fact]
    public async Task post_create_vehicle_without_authentication_should_return_401_status_code()
    {
        //arrange
        var command = _factory.CreateVehicleCommand();

        //act
        var response = await Client.PostAsJsonAsync(VehicleEndpoints.BasePath, command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }


    [Fact]
    public async Task post_create_vehicle_with_valid_data_should_return_201_status_code()
    {
        //arrange
        var command = _factory.CreateVehicleCommand();
        var user = _factory.CreateUser();

        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
        Authorize(user.Id, user.Role.ToString());

        //act
        var response = await Client.PostAsJsonAsync(VehicleEndpoints.BasePath, command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
    }


    private readonly VehicleTestFactory _factory = new();
}