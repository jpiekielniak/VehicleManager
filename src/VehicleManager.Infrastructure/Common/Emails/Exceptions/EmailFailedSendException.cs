using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Infrastructure.Common.Emails.Exceptions;

public class EmailFailedSendException(string email) 
    : VehicleManagerException($"Failed to send email to {email}");