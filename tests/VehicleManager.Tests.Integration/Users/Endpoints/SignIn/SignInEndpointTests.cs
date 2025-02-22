using VehicleManager.Api.Endpoints.Users;
using VehicleManager.Application.Users.Commands.SignIn;
using VehicleManager.Infrastructure.Common.Middlewares.Exceptions.Models;
using VehicleManager.Tests.Integration.Users.Factories;

namespace VehicleManager.Tests.Integration.Users.Endpoints.SignIn;

public class SignInEndpointTests : UserEndpointTest
{
    [Fact]
    public async Task post_sign_in_with_valid_data_should_return_200_status_code()
    {
        //arrange
        var command = _factory.CreateSignInCommand();
        var hashedPassword = PasswordHasher.HashPassword(command.Password);
        var user = _factory.CreateUser(command.Email, hashedPassword);
        await SeedDataAsync(user);
        
        //act
        var response = await Client.PostAsJsonAsync(UserEndpoints.SignIn, command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        await response.Content.ReadFromJsonAsync<SignInResponse>().ShouldNotBeNull();
    }

    [Fact]
    public async Task post_sign_in_with_non_existing_user_data_should_return_400_status_code()
    {
        //arrange
        var command = _factory.CreateSignInCommand();

        //act
        var response = await Client.PostAsJsonAsync(UserEndpoints.SignIn, command);

        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        await response.Content.ReadFromJsonAsync<Error>().ShouldNotBeNull();
    }

    private readonly UserTestFactory _factory = new();
}