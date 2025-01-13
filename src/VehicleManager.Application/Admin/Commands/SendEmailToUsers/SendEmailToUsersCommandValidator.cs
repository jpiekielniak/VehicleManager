namespace VehicleManager.Application.Admin.Commands.SendEmailToUsers;

internal sealed class SendEmailToUsersCommandValidator : AbstractValidator<SendEmailToUsersCommand>
{
    public SendEmailToUsersCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("The title must not be empty.")
            .MaximumLength(100)
            .WithMessage("The length of title must be 100 characters or fewer.");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content must not be empty.")
            .MaximumLength(1500)
            .WithMessage("The length of content must be 1500 characters or fewer.");
    }
}