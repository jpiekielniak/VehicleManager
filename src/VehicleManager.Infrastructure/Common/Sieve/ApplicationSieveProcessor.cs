namespace VehicleManager.Infrastructure.Common.Sieve;

public class ApplicationSieveProcessor(IOptions<SieveOptions> options) : SieveProcessor(options)
{
    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        mapper.ApplyConfigurationsFromAssembly(typeof(ApplicationSieveProcessor).Assembly);
        return mapper;
    }
}