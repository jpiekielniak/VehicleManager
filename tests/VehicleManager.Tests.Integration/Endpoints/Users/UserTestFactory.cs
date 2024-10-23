using VehicleManager.Application.Users.Commands.SignIn;
using VehicleManager.Application.Users.Commands.SignUp;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Entities.Enums;

namespace VehicleManager.Tests.Integration.Endpoints.Users;

internal class UserTestFactory
{
    private readonly Faker _faker = new();

    internal SignUpCommand CreateSignUpCommand()
        => new(
            _faker.Internet.Email(),
            _faker.Internet.Password()
        );

    internal SignInCommand CreateSignInCommand()
        => new(
            _faker.Internet.Email(),
            _faker.Internet.Password()
        );

    internal User CreateUser(string? email = default, string? password = default)
        => new UserBuilder()
            .WithEmail(email ?? _faker.Internet.Email())
            .WithPassword(password ?? _faker.Internet.Password())
            .WithRole(Role.User)
            .Build();
}