using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Core.Vehicles.Builders;

public class ServiceBuilder(Service service)
{
    public ServiceBuilder() : this(Service.Create())
    {
    }

    public Service Build() => service;

    public ServiceBuilder WithTitle(string title)
    {
        ArgumentNullException.ThrowIfNull(title);
        service.Title = title;
        return this;
    }

    public ServiceBuilder WithDescription(string description)
    {
        service.Description = description;
        return this;
    }

    public ServiceBuilder WithServiceDate(DateTimeOffset serviceDate)
    {
        ArgumentNullException.ThrowIfNull(serviceDate);
        service.Date = serviceDate;
        return this;
    }

    public ServiceBuilder WithServiceBook(ServiceBook serviceBook)
    {
        ArgumentNullException.ThrowIfNull(serviceBook);
        service.ServiceBook = serviceBook;
        service.ServiceBookId = serviceBook.Id;
        return this;
    }
}