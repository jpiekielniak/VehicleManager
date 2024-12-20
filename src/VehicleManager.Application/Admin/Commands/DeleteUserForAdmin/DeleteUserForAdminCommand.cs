namespace VehicleManager.Application.Admin.Commands.DeleteUserForAdmin;

internal record DeleteUserForAdminCommand(Guid UserId) : IRequest;