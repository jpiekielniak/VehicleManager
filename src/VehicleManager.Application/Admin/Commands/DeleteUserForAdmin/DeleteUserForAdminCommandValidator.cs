namespace VehicleManager.Application.Admin.Commands.DeleteUserForAdmin;

internal sealed class DeleteUserForAdminCommandValidator : AbstractValidator<DeleteUserForAdminCommand>
{
    public DeleteUserForAdminCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("User id not be equal to empty guid");
    }
}