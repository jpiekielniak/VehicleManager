using CarManagement.Core.Users.Entities;
using CarManagement.Core.Users.Entities.Builders;
using CarManagement.Test.Unit.Users.Entities.Builders.TestData.Invalid;
using CarManagement.Test.Unit.Users.Entities.Builders.TestData.Valid;

namespace CarManagement.Test.Unit.Users.Entities.Builders;

public class UserBuilderTests
{
    [Theory]
    [ClassData(typeof(UserBuilderValidTestData))]
    public void given_valid_data_to_builder_should_build_user_success(string userName, string email,
        string phoneNumber, string password, Role role)
    {
        //act
        var user = new UserBuilder()
            .WithUsername(userName)
            .WithPhoneNumber(phoneNumber)
            .WithEmail(email)
            .WithPassword(password)
            .WithRole(role)
            .Build();

        //assert
        user.ShouldNotBeNull();
        user.Id.ShouldNotBeSameAs(Guid.Empty);
    }

    [Theory]
    [ClassData(typeof(UserBuilderInvalidTestData))]
    public void given_invalid_data_to_builder_should_build_user_fail(string userName, string email,
        string phoneNumber, string password, Role role)
    {
        //act
        var exception = Record.Exception(
            () => new UserBuilder()
                .WithUsername(userName)
                .WithPhoneNumber(phoneNumber)
                .WithEmail(email)
                .WithPassword(password)
                .WithRole(role)
                .Build()
        );

        //assert
        exception.ShouldNotBeNull();
        exception.ShouldBeAssignableTo<ArgumentException>();
    }
}