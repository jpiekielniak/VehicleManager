using VehicleManager.Api.Endpoints.Users;
using VehicleManager.Application.Users.Commands.SignUp;
using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Shared.Middlewares.Exceptions;

namespace VehicleManager.Tests.Integration.Endpoints.Users.Commands.SignUp;

public class SignUpEndpointTests(VehicleManagerTestFactory factory) : EndpointTests(factory)
{
    [Fact]
    public async Task post_sign_up_with_valid_data_should_return_201()
    {
        // Arrange
        var command = new SignUpCommand(
            "car.managment@test.com",
            "Jakub",
            "Piekielniak",
            "512839855",
            "password"
        );

        // Act
        var response = await Client.PostAsJsonAsync(UserEndpoints.SignUp, command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task post_sign_up_with_existing_email_should_return_400()
    {
        // Arrange
        const string email = "car.managment@test.com";
        var user = new UserBuilder()
            .WithFirstName("Jakub")
            .WithLastName("Piekielniak")
            .WithEmail("car.managment@test.com")
            .WithPassword("password")
            .WithRole(Role.User)
            .WithPhoneNumber("512839855")
            .Build();

        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();

        var command = new SignUpCommand(
            email,
            "Jakub",
            "Piekielniak",
            "512839855",
            "password"
        );

        // Act
        var response = await Client.PostAsJsonAsync(UserEndpoints.SignUp, command);

        // Assert
        var error = response.Content.ReadFromJsonAsync<Error>().Result;
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        error?.Message.ShouldBe("Email already exists");
    }
}