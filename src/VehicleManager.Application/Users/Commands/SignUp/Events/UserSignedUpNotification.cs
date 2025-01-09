namespace VehicleManager.Application.Users.Commands.SignUp.Events;

public record UserSignedUpNotification(string Email) : INotification;
