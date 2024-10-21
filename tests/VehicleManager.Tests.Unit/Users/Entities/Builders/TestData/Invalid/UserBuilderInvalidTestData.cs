using VehicleManager.Core.Users.Entities;
using VehicleManager.Core.Users.Entities.Enums;

namespace VehicleManager.Tests.Unit.Users.Entities.Builders.TestData.Invalid;

internal class UserBuilderInvalidTestData : TheoryData<UserBuilderParams>
{

    public UserBuilderInvalidTestData()
    {
        Add(new UserBuilderParams());

        Add(new UserBuilderParams
        {
            FirstName = "",
            LastName = "",
            Email = "",
            Password = "",
            PhoneNumber = "",
            Role = Role.User
        });

        Add(new UserBuilderParams
        {
            FirstName = "Firstname",
            LastName = "LastName",
            Email = "",
            Password = "",
            PhoneNumber = "",
            Role = Role.Admin
        });

        Add(new UserBuilderParams
        {
            FirstName = "Firstname",
            LastName = "LastName",
            Email = "email",
            Password = "",
            PhoneNumber = "",
            Role = Role.Admin
        });

        Add(new UserBuilderParams
        {
            FirstName = "      ",
            LastName = "LastName",
            Email = "email",
            Password = "password",
            PhoneNumber = "123456789",
            Role = Role.User
        });
    }
}