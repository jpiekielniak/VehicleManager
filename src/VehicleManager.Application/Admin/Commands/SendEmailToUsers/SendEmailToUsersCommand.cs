namespace VehicleManager.Application.Admin.Commands.SendEmailToUsers;

internal record SendEmailToUsersCommand(string Title, string Content) : IRequest;