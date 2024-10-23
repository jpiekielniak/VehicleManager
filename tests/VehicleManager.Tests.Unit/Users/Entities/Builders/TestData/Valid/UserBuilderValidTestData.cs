using VehicleManager.Core.Users.Entities.Enums;

namespace VehicleManager.Tests.Unit.Users.Entities.Builders.TestData.Valid;

internal class UserBuilderValidTestData : TheoryData<UserBuilderParams>
{
    private readonly Faker _faker = new();

    public UserBuilderValidTestData()
    {
        Add(new UserBuilderParams
        {
            Email = _faker.Internet.Email(),
            FirstName = _faker.Person.FirstName,
            LastName = _faker.Person.LastName,
            Password = _faker.Internet.Password(),
            PhoneNumber = _faker.Phone.PhoneNumber(),
            Role = Role.User
        });

        Add(new UserBuilderParams
        {
            Email = _faker.Internet.Email(),
            FirstName = _faker.Person.FirstName,
            LastName = _faker.Person.LastName,
            Password = _faker.Internet.Password(),
            PhoneNumber = _faker.Phone.PhoneNumber(),
            Role = Role.User
        });

        Add(new UserBuilderParams
        {
            Email = _faker.Internet.Email(),
            FirstName = _faker.Person.FirstName,
            LastName = _faker.Person.LastName,
            Password = _faker.Internet.Password(),
            PhoneNumber = _faker.Phone.PhoneNumber(),
            Role = Role.User
        });
    }
}