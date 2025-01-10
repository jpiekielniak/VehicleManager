namespace VehicleManager.Application.Users.Commands.DeleteUser.Events;

public record UserDeletedNotification(string Email) : INotification;
