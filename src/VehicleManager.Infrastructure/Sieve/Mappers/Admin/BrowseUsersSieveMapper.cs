using VehicleManager.Core.Users.Entities;

namespace VehicleManager.Infrastructure.Sieve.Mappers.Admin;

internal sealed class BrowseUsersSieveMapper : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<User>(u => u.Email)
            .CanFilter()
            .CanSort()
            .HasName("email");
    }
}