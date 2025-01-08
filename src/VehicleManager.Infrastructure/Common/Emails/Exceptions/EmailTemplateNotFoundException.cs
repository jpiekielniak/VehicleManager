using VehicleManager.Core.Common.Exceptions;

namespace VehicleManager.Infrastructure.Common.Emails.Exceptions;

public class EmailTemplateNotFoundException(string templateName) 
    : VehicleManagerException($"Could not find email template: {templateName}");