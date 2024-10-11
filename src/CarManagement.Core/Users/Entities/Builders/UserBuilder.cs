namespace CarManagement.Core.Users.Entities.Builders;

internal sealed class UserBuilder
{
    private readonly User _user = User.Create();

    public User Build() => _user;

    public UserBuilder WithUserName(string userName)
    {
        _user.Username = userName;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public UserBuilder WithPassword(string password)
    {
        _user.Password = password;
        return this;
    }

    public UserBuilder WithPhoneNumber(string phoneNumber)
    {
        _user.PhoneNumber = phoneNumber;
        return this;
    }

    public UserBuilder WithRole(Role role)
    {
        _user.Role = role;
        return this;
    }
}