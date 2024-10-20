using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Entities.Builders;
using VehicleManager.Tests.Unit.Users.Entities.Builders.TestData.Invalid;
using VehicleManager.Tests.Unit.Users.Entities.Builders.TestData.Valid;

namespace VehicleManager.Tests.Unit.Users.Entities.Builders;

public class UserBuilderTests
{
    [Theory]
    [ClassData(typeof(UserBuilderValidTestData))]
    public void given_valid_data_to_builder_should_build_user_success(
        UserBuilderParams userBuilderParams
    )
    {
        //act
        var user = Act(userBuilderParams);

        //assert
        user.ShouldNotBeNull();
        user.Id.ShouldNotBeSameAs(Guid.Empty);
    }

    [Theory]
    [ClassData(typeof(UserBuilderInvalidTestData))]
    public void given_invalid_data_to_builder_should_build_user_fail(
        UserBuilderParams userBuilderParams
    )
    {
        //act
        var exception = Record.Exception(() => Act(userBuilderParams));

        //assert
        exception.ShouldNotBeNull();
        exception.ShouldBeAssignableTo<ArgumentException>();
    }

    private static User Act(UserBuilderParams userBuilderParams)
        => new UserBuilder()
            .WithFirstName(userBuilderParams.FirstName)
            .WithLastName(userBuilderParams.LastName)
            .WithPhoneNumber(userBuilderParams.PhoneNumber)
            .WithEmail(userBuilderParams.Email)
            .WithPassword(userBuilderParams.Password)
            .WithRole(userBuilderParams.Role)
            .Build();
}