using VehicleManager.Application.Admin.Commands.DeleteUserForAdmin;
using VehicleManager.Application.Admin.Commands.SendEmailToUsers;
using VehicleManager.Core.Users.Entities;

namespace VehicleManager.Tests.Unit.Admin.Factories;

internal class AdminTestFactory
{
    private readonly Faker _faker = new();

    public DeleteUserForAdminCommand CreateDeleteUserForAdminCommand(Guid? userId = null)
        => new(userId ?? Guid.NewGuid());

    public SendEmailToUsersCommand CreateSendEmailToUsersCommand()
    => new(_faker.Lorem.Sentence(), _faker.Lorem.Paragraph());

    public List<User> CreateUsers(int count)
    => Enumerable
        .Range(0, count)
        .Select(_ => User.Create())
        .ToList();
}