using VehicleManager.Application.Common.Models;
using VehicleManager.Application.Users.Commands.CompleteUserData;
using VehicleManager.Application.Users.Commands.DeleteUser;
using VehicleManager.Application.Users.Commands.SignIn;
using VehicleManager.Application.Users.Commands.SignUp;
using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Core.Users.Entities.Enums;
using VehicleManager.Tests.Unit.Users.Helpers;

namespace VehicleManager.Tests.Unit.Users.Factories;

internal class UserTestFactory
{
    private readonly Faker _faker = new();

    internal SignInCommand CreateSignInCommand()
        => new(
            _faker.Internet.Email(),
            _faker.Internet.Password()
        );

    internal SignUpCommand CreateSignUpCommand() => new(
        _faker.Internet.Email(),
        _faker.Internet.Password()
    );


    internal User CreateUser() => new UserBuilder()
        .WithEmail(_faker.Internet.Email())
        .WithFirstName(_faker.Person.FirstName)
        .WithLastName(_faker.Person.LastName)
        .WithPhoneNumber(_faker.Phone.PhoneNumber())
        .WithPassword(_faker.Internet.Password())
        .WithRole(Role.User)
        .Build();

    internal JsonWebToken CreateToken(Guid userId, string role)
        => JwtHelper.CreateToken(userId.ToString(), role);

    public DeleteUserCommand CreateDeleteUserCommand()
        => new();

    public CompleteUserDataCommand CreateCompleteUserDataCommand(Guid? userId = default)
        => new(
            _faker.Person.FirstName,
            _faker.Person.LastName,
            _faker.Phone.PhoneNumber()
        )
        {
            UserId = userId ?? Guid.NewGuid()
        };
}