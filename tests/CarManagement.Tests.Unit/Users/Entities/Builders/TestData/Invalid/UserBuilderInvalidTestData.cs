using CarManagement.Core.Users.Entities;

namespace CarManagement.Tests.Unit.Users.Entities.Builders.TestData.Invalid;

internal class UserBuilderInvalidTestData : TheoryData<UserBuilderParams>
{
    private static Role Role => Role.Create("role");

    public UserBuilderInvalidTestData()
    {
        Add(new UserBuilderParams());

        Add(new UserBuilderParams
        {
            Username = "",
            Email = "",
            Password = "",
            PhoneNumber = "",
            Role = null
        });

        Add(new UserBuilderParams
        {
            Username = "username",
            Email = "",
            Password = "",
            PhoneNumber = "",
            Role = null
        });

        Add(new UserBuilderParams
        {
            Username = "username",
            Email = "email",
            Password = "",
            PhoneNumber = "",
            Role = null
        });

        Add(new UserBuilderParams
        {
            Username = "     ",
            Email = "email",
            Password = "password",
            PhoneNumber = "123456789",
            Role = Role
        });
    }
}