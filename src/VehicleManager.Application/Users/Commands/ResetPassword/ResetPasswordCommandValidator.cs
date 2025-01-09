namespace VehicleManager.Application.Users.Commands.ResetPassword;

internal sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(x => x.NewPassword)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.Token)
            .NotEmpty()
            .NotNull();
    }
}