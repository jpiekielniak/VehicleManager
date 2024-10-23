using VehicleManager.Core.Users.Entities.Enums;

namespace VehicleManager.Tests.Unit.Users.Entities.Builders.TestData.Invalid;

internal class UserBuilderInvalidTestData : TheoryData<UserBuilderParams>
{
    private readonly Faker _faker = new();

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
            FirstName = _faker.Person.FirstName,
            LastName = _faker.Person.LastName,
            Email = "",
            Password = "",
            PhoneNumber = "",
            Role = Role.Admin
        });

        Add(new UserBuilderParams
        {
            FirstName = _faker.Person.FirstName,
            LastName = _faker.Person.LastName,
            Email = _faker.Internet.Email(),
            Password = "",
            PhoneNumber = "",
            Role = Role.Admin
        });

        Add(new UserBuilderParams
        {
            FirstName = "      ",
            LastName = _faker.Person.LastName,
            Email = _faker.Internet.Email(),
            Password = _faker.Internet.Password(),
            PhoneNumber = _faker.Phone.PhoneNumber(),
            Role = Role.User
        });
    }
}