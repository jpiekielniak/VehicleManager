using CarManagement.Application.Users.Commands.SignIn;
using CarManagement.Application.Users.Commands.SignUp;
using CarManagement.Core.Users.Entities;
using CarManagement.Core.Users.Entities.Builders;
using CarManagement.Shared.Auth;
using CarManagement.Tests.Unit.Users.Helpers;

namespace CarManagement.Tests.Unit.Users.Factories;

public class UserTestFactory
{
    internal SignInCommand CreateSignInCommand()
        => new("car.management@test.com", "password");

    internal SignUpCommand CreateSignUpCommand() => new(
        "car.management@test.com",
        "car.management.",
        "123456789",
        "password",
        "password"
    );

    internal Role CreateRole() => Role.Create("User");

    internal User CreateUser() => new UserBuilder()
        .WithEmail("car.management@test.com")
        .WithUsername("carmanagement")
        .WithPhoneNumber("123456789")
        .WithPassword("password")
        .WithRole(CreateRole())
        .Build();

    internal JsonWebToken CreateToken(Guid userId, string role)
        => JwtHelper.CreateToken(userId.ToString(), role);
}