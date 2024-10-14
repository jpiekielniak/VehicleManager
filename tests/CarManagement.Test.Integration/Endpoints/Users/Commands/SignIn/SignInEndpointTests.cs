using CarManagement.Api.Endpoints.Users;
using CarManagement.Application.Users.Commands.SignIn;
using CarManagement.Core.Users.Entities.Builders;
using CarManagement.Shared.Middlewares.Exceptions;

namespace CarManagement.Test.Integration.Endpoints.Users.Commands.SignIn;

public class SignInEndpointTests(CarManagementTestFactory factory) : EndpointTests(factory)
{
    [Fact]
    public async Task post_sign_in_with_valid_data_should_return_200_status_code()
    {
        //arrange
        const string email = "car.managemenet@test.com";
        const string password = "passwordtest";
        var hashedPassword = PasswordHasher.HashPassword(password);

        var user = new UserBuilder()
            .WithEmail(email)
            .WithPassword(hashedPassword)
            .WithUsername("carmanagement")
            .WithRole(Role)
            .WithPhoneNumber("123456789")
            .Build();

        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();

        var command = new SignInCommand(email, password);

        //act
        var response = await Client.PostAsJsonAsync(UserEndpoints.SignIn, command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        await response.Content.ReadFromJsonAsync<SignInResponse>().ShouldNotBeNull();
    }

    [Fact]
    public async Task post_sign_in_with_invalid_data_should_return_400_status_code()
    {
        //arrange
        var command = new SignInCommand("example@test.com", "examplePassword");

        //act
        var response = await Client.PostAsJsonAsync(UserEndpoints.SignIn, command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        await response.Content.ReadFromJsonAsync<Error>().ShouldNotBeNull();
    }
}