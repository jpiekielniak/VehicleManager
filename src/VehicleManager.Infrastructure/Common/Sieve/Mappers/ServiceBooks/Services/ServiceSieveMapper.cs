using VehicleManager.Core.Vehicles.Entities;

namespace VehicleManager.Infrastructure.Common.Sieve.Mappers.ServiceBooks.Services;

internal sealed class ServiceSieveMapper : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Service>(s => s.Title)
            .CanFilter()
            .CanSort()
            .HasName("title");

        mapper.Property<Service>(s => s.Date)
            .CanFilter()
            .CanSort()
            .HasName("date");
    }
}