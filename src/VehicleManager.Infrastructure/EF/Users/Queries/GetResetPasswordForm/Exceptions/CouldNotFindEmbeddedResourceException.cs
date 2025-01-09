using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Infrastructure.EF.Users.Queries.GetResetPasswordForm.Exceptions;

internal class CouldNotFindEmbeddedResourceException(string embeddedResource)
    : VehicleManagerException($"Could not find embedded resource: {embeddedResource}.");