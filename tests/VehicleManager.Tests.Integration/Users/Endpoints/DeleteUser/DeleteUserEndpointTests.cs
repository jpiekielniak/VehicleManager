using VehicleManager.Api.Endpoints.Users;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Tests.Integration.Users.Factories;

namespace VehicleManager.Tests.Integration.Users.Endpoints.DeleteUser;

public class DeleteUserEndpointTests : UserEndpointTest
{
    [Fact]
    public async Task delete_user_without_authentication_should_return_401_status_code()
    {
        //Act
        var response =
            await Client.DeleteAsync(UserEndpoints.UserById.Replace("{userId:guid}", Guid.NewGuid().ToString()));

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task delete_user_with_non_existing_user_should_return_400_status_code()
    {
        //Arrange
        Authorize(Guid.NewGuid(), Role.User.ToString());

        //Act
        var response =
            await Client.DeleteAsync(UserEndpoints.UserById.Replace("{userId:guid}", Guid.NewGuid().ToString()));

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task delete_user_with_different_context_id_should_return_400_status_code()
    {
        //Arrange
        var user = _factory.CreateUser();
        await SeedDataAsync(user);
        Authorize(Guid.NewGuid(), Role.User.ToString());

        //Act
        var response = await Client.DeleteAsync(UserEndpoints.UserById.Replace("{userId:guid}", user.Id.ToString()));

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task delete_user_with_existing_user_should_return_204_status_code()
    {
        //Arrange
        var user = _factory.CreateUser();
        await SeedDataAsync(user);
        Authorize(user.Id, Role.User.ToString());

        //Act
        var response = await Client.DeleteAsync(UserEndpoints.UserById.Replace("{userId:guid}", user.Id.ToString()));

        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }


    private readonly UserTestFactory _factory = new();
}