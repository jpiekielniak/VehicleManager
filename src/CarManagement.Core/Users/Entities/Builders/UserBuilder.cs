namespace CarManagement.Core.Users.Entities.Builders;

public sealed class UserBuilder
{
    private readonly User _user = User.Create();

    public User Build() => _user;

    public UserBuilder WithUsername(string userName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);
        _user.Username = userName;
        return this;
    }

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
}