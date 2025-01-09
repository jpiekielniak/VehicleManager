using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Infrastructure.EF.Users.Queries.GetResetPasswordForm.Exceptions;

internal class InvalidPasswordResetTokenException() : VehicleManagerException("Invalid password reset token.");