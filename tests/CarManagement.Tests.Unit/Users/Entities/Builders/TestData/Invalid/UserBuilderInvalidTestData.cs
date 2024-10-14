using CarManagement.Core.Users.Entities;

namespace CarManagement.Tests.Unit.Users.Entities.Builders.TestData.Invalid;

public class UserBuilderInvalidTestData : TheoryData<string, string, string, string, Role>
{
    private static Role Role => Role.Create("role");

    public UserBuilderInvalidTestData()
    {
        Add(null, null, null, null, null);
        Add("", "", "", "", null);
        Add("username", "", "", "", null);
        Add("", "", "password", "", null);
        Add("", "", "", "123456789", null);
        Add("  ", "car.management@email.com", "password", "123456789", Role);
    }
}