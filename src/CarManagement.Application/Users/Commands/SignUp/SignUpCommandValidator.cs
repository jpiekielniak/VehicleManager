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

        RuleFor(x => x.Email)
            .CustomAsync(async (email, context, cancellationToken) =>
            {
                var userExists = await userRepository
                    .AnyAsync(
                        x => x.Email == email,
                        cancellationToken
                    );

                if (userExists)
                {
                    throw new EmailAlreadyExistsException(email);
                }
            });

        RuleFor(x => x.Username)
            .CustomAsync(async (userName, context, cancellationToken) =>
            {
                var userExists = await userRepository
                    .AnyAsync(
                        x => x.Username == userName,
                        cancellationToken
                    );

                if (userExists)
                {
                    throw new UsernameAlreadyExistsExceptions(userName);
                }
            });
    }
}