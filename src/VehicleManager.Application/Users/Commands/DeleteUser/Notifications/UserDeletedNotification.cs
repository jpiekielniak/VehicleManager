namespace VehicleManager.Application.Users.Commands.DeleteUser.Notifications;

public record UserDeletedNotification(string Email) : INotification;
