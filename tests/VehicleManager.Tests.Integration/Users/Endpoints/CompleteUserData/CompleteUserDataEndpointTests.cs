using VehicleManager.Api.Endpoints.Users;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Shared.Middlewares.Exceptions;
using VehicleManager.Tests.Integration.Users.Factories;

namespace VehicleManager.Tests.Integration.Users.Endpoints.CompleteUserData;

public class CompleteUserDataEndpointTests : EndpointTests
{
    [Fact]
    public async Task put_complete_user_data_without_authentication_should_return_401_status_code()
    {
        // Arrange
        var command = _factory.CreateCompleteUserDataCommand();

        // Act
        var response =
            await Client.PutAsJsonAsync(
                UserEndpoints.CompleteUserData.Replace("{userId:guid}", Guid.NewGuid().ToString()),
                command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task put_complete_user_data_with_user_id_not_matching_context_id_should_return_400_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        await SeedDataAsync(user);
        Authorize(Guid.NewGuid(), Role.User.ToString());
        var command = _factory.CreateCompleteUserDataCommand(user.Id);

        // Act
        var response =
            await Client.PutAsJsonAsync(UserEndpoints.CompleteUserData.Replace("{userId:guid}", user.Id.ToString()),
                command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var content = await response.Content.ReadFromJsonAsync<Error>();
        content.ShouldNotBeNull();
    }

    [Fact]
    public async Task put_complete_user_data_with_valid_data_should_return_204_status_code()
    {
        // Arrange
        var user = _factory.CreateUser();
        await SeedDataAsync(user);
        Authorize(user.Id, user.Role.ToString());
        var command = _factory.CreateCompleteUserDataCommand(user.Id);

        // Act
        var response =
            await Client.PutAsJsonAsync(UserEndpoints.CompleteUserData.Replace("{userId:guid}", user.Id.ToString()),
                command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }


    private readonly UserTestFactory _factory = new();
}