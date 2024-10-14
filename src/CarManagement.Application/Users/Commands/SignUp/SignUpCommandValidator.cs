using CarManagement.Core.Users.Exceptions.Users;
using CarManagement.Core.Users.Repositories;

namespace CarManagement.Application.Users.Commands.SignUp;

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

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password cannot be empty")
            .NotNull().WithMessage("Confirm password cannot be null")
            .Equal(x => x.Password).WithMessage("Confirm password must be equal to password");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty")
            .NotNull().WithMessage("Username cannot be null")
            .MaximumLength(150).WithMessage("Username must be at most 150 characters long");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number cannot be empty")
            .NotNull().WithMessage("Phone number cannot be null")
            .Matches(@"^\d{9}$").WithMessage("Phone number must be 9 digits long");

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

        RuleFor(x => x.Username)
            .MustAsync(async (userName, cancellationToken) =>
            {
                var userExists = await userRepository
                    .AnyAsync(
                        x => x.Username == userName,
                        cancellationToken
                    );

                return !userExists;
            }).WithMessage("Username already exists");
    }
}