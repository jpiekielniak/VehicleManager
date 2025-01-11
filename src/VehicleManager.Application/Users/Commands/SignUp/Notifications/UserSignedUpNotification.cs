namespace VehicleManager.Application.Users.Commands.SignUp.Notifications;

public record UserSignedUpNotification(string Email) : INotification;
