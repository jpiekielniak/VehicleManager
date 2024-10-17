using CarManagement.Core.Users.Entities;

namespace CarManagement.Tests.Unit.Users.Entities.Builders.TestData.Valid;

internal class UserBuilderValidTestData : TheoryData<UserBuilderParams>
{
    private static Role Role => Role.Create("role");

    public UserBuilderValidTestData()
    {
        Add(new UserBuilderParams
        {
            Email = "car.management@test.com",
            Username = "username",
            Password = "password",
            PhoneNumber = "123456789",
            Role = Role
        });

        Add(new UserBuilderParams
        {
            Email = "car.management2@test.com",
            Username = "username2",
            Password = "password2",
            PhoneNumber = "987654321",
            Role = Role
        });

        Add(new UserBuilderParams
        {
            Email = "car.management3@test.com",
            Username = "username3",
            Password = "password3",
            PhoneNumber = "549821435",
            Role = Role
        });
    }
}