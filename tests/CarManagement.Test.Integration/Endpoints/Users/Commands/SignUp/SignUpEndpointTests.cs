using CarManagement.Application.Users.Commands.SignUp;
using CarManagement.Core.Users.Entities.Builders;
using CarManagement.Shared.Middlewares.Exceptions;

namespace CarManagement.Test.Integration.Endpoints.Users.Commands.SignUp;

public class SignUpEndpointTests(CarManagementTestFactory factory) : EndpointTests(factory)
{
    [Fact]
    public async Task post_sign_up_with_valid_data_should_return_201()
    {
        // Arrange
        var command = new SignUpCommand("car.managment@test.com", "carmanagment", "123456789", "password", "password");

        // Act
        var response = await Client.PostAsJsonAsync("/api/v1/users/sign-up", command);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task post_sign_up_with_existing_email_should_return_400()
    {
        // Arrange
        const string email = "car.managment@test.com";
        var user = new UserBuilder()
            .WithUsername("carmanagment")
            .WithEmail("car.managment@test.com")
            .WithPassword("password")
            .WithRole(Role)
            .WithPhoneNumber("123456789")
            .Build();

        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();

        var command = new SignUpCommand(email, "carmanagmenttest", "123456789", "password", "password");

        // Act
        var response = await Client.PostAsJsonAsync("/api/v1/users/sign-up", command);

        // Assert
        var error = response.Content.ReadFromJsonAsync<Error>().Result;
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        error?.Message.ShouldBe($"User with email {email} already exists");
    }
}