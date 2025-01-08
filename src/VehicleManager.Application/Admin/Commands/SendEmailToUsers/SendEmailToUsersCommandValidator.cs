namespace VehicleManager.Application.Admin.Commands.SendEmailToUsers;

internal sealed class SendEmailToUsersCommandValidator : AbstractValidator<SendEmailToUsersCommand>
{
    public SendEmailToUsersCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1500);
    }
}