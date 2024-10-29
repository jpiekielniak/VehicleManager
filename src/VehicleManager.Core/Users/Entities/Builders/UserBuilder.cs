using VehicleManager.Core.Users.Entities.Enums;

namespace VehicleManager.Core.Users.Entities.Builders;

public sealed class UserBuilder(User user)
{

    public User Build() => user;
    
    public UserBuilder() : this(User.Create())
    {
    }

    public UserBuilder WithEmail(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        user.Email = email;
        return this;
    }

    public UserBuilder WithPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        user.Password = password;
        return this;
    }

    public UserBuilder WithPhoneNumber(string phoneNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);
        user.PhoneNumber = phoneNumber;
        return this;
    }

    public UserBuilder WithRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);
        user.Role = role;
        return this;
    }

    public UserBuilder WithFirstName(string firstName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        user.FirstName = firstName;
        return this;
    }

    public UserBuilder WithLastName(string lastName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        user.LastName = lastName;
        return this;
    }
}