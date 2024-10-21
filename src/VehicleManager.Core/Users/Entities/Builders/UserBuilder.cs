using VehicleManager.Core.Users.Entities.Enums;

namespace VehicleManager.Core.Users.Entities.Builders;

public sealed class UserBuilder
{
    private readonly User _user = User.Create();

    public User Build() => _user;

    public UserBuilder WithEmail(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        _user.Email = email;
        return this;
    }

    public UserBuilder WithPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        _user.Password = password;
        return this;
    }

    public UserBuilder WithPhoneNumber(string phoneNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);
        _user.PhoneNumber = phoneNumber;
        return this;
    }

    public UserBuilder WithRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        _user.Role = role;
        return this;
    }

    public UserBuilder WithFirstName(string firstName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        _user.FirstName = firstName;
        return this;
    }

    public UserBuilder WithLastName(string lastName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        _user.LastName = lastName;
        return this;
    }
}