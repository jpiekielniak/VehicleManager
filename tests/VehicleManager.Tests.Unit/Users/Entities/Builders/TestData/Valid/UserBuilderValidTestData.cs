using VehicleManager.Core.Users.Entities;

namespace VehicleManager.Tests.Unit.Users.Entities.Builders.TestData.Valid;

internal class UserBuilderValidTestData : TheoryData<UserBuilderParams>
{
    private static Role Role => Role.Create("role");

    public UserBuilderValidTestData()
    {
        Add(new UserBuilderParams
        {
            Email = "car.management@test.com",
            FirstName = "Firstname",
            LastName = "Lastname",
            Password = "password",
            PhoneNumber = "123456789",
            Role = Role
        });

        Add(new UserBuilderParams
        {
            Email = "car.management2@test.com",
            FirstName = "Firstname2",
            LastName = "Lastname2",
            Password = "password2",
            PhoneNumber = "987654321",
            Role = Role
        });

        Add(new UserBuilderParams
        {
            Email = "car.management3@test.com",
            FirstName = "Firstname3",
            LastName = "Lastname3",
            Password = "password3",
            PhoneNumber = "549821435",
            Role = Role
        });
    }
}