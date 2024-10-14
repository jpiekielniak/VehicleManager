using CarManagement.Core.Users.Entities;

namespace CarManagement.Tests.Unit.Users.Entities.Builders.TestData.Valid;

public class UserBuilderValidTestData : TheoryData<string, string, string, string, Role>
{
    private static Role Role => Role.Create("role");

    public UserBuilderValidTestData()
    {
        Add("username", "car.management2@test.com", "password", "123456789", Role);
        Add("username2", "car.management2@test.com", "password2", "987654321", Role);
        Add("username3", "car.management@test.com", "password3", "549821435", Role);
    }
}