using VehicleManager.Application.Users.Commands.SignIn;
using VehicleManager.Application.Users.Commands.SignUp;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Shared.Auth;
using VehicleManager.Tests.Unit.Users.Helpers;

namespace VehicleManager.Tests.Unit.Users.Factories;

public class UserTestFactory
{
    internal SignInCommand CreateSignInCommand()
        => new("car.management@test.com", "password");

    internal SignUpCommand CreateSignUpCommand() => new(
        "car.management@test.com",
        "Jakub",
        "Piekielniak",
        "512839855",
        "password"
    );


    internal User CreateUser() => new UserBuilder()
        .WithEmail("car.management@test.com")
        .WithFirstName("Jakub")
        .WithLastName("Piekielniak")
        .WithPhoneNumber("512839855")
        .WithPassword("password")
        .WithRole(Role.User)
        .Build();

    internal JsonWebToken CreateToken(Guid userId, string role)
        => JwtHelper.CreateToken(userId.ToString(), role);
}