using VehicleManager.Core.Users.Entities;

namespace VehicleManager.Tests.Unit.Users.Entities.Builders.TestData.Invalid;

internal class UserBuilderInvalidTestData : TheoryData<UserBuilderParams>
{
    private static Role Role => Role.Create("role");

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
            Role = null
        });

        Add(new UserBuilderParams
        {
            FirstName = "Firstname",
            LastName = "LastName",
            Email = "",
            Password = "",
            PhoneNumber = "",
            Role = null
        });

        Add(new UserBuilderParams
        {
            FirstName = "Firstname",
            LastName = "LastName",
            Email = "email",
            Password = "",
            PhoneNumber = "",
            Role = null
        });

        Add(new UserBuilderParams
        {
            FirstName = "      ",
            LastName = "LastName",
            Email = "email",
            Password = "password",
            PhoneNumber = "123456789",
            Role = Role
        });
    }
}