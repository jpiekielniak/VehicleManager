using VehicleManager.Core.Users.Repositories;

namespace VehicleManager.Application.Users.Commands.SignUp;

internal sealed class SignUpCommandValidator
    : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .NotNull().WithMessage("Email cannot be null")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .NotNull().WithMessage("Password cannot be null")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .MaximumLength(16).WithMessage("Password must be at most 16 characters long");

        RuleFor(x => x.Email)
            .MustAsync(async (email, cancellationToken) =>
            {
                var userExists = await userRepository.AnyAsync(
                    x => x.Email == email,
                    cancellationToken
                );

                return !userExists;
            })
            .WithMessage("Email already exists");
       
    }
}