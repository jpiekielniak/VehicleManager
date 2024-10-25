using VehicleManager.Api.Endpoints.Users;
using VehicleManager.Shared.Middlewares.Exceptions;
using VehicleManager.Tests.Integration.Users.Factories;

namespace VehicleManager.Tests.Integration.Users.Endpoints.SignUp;

public class SignUpEndpointTests(VehicleManagerTestFactory factory) : EndpointTests(factory)
{
    [Fact]
    public async Task post_sign_up_with_valid_data_should_return_201()
    {
        // Arrange
        var command = _factory.CreateSignUpCommand();

        // Act
        var response = await Client.PostAsJsonAsync(UserEndpoints.SignUp, command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task post_sign_up_with_existing_email_should_return_400()
    {
        // Arrange
        var user = _factory.CreateUser();

        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();

        var command = _factory.CreateSignUpCommand() with { Email = user.Email };

        // Act
        var response = await Client.PostAsJsonAsync(UserEndpoints.SignUp, command);

        // Assert
        var error = response.Content.ReadFromJsonAsync<Error>().Result;
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        error?.Message.ShouldBe("Email already exists");
    }

    private readonly UserTestFactory _factory = new();
}