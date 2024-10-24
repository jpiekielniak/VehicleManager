using VehicleManager.Application.ServiceBooks.Queries.GetService;
using VehicleManager.Application.ServiceBooks.Queries.GetService.DTO;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.ServiceBooks.Queries.GetService;

internal sealed class GetServiceQueryHandler(
    IServiceBookRepository serviceBookRepository
) : IRequestHandler<GetServiceQuery, ServiceDetailsDto>
{
    public async Task<ServiceDetailsDto> Handle(GetServiceQuery query,
        CancellationToken cancellationToken)
    {
        var serviceBook = await serviceBookRepository
                              .GetAsync(query.ServiceBookId, cancellationToken, true)
                          ?? throw new ServiceBookNotFoundException(query.ServiceBookId);

        var service = serviceBook.Services
                          .FirstOrDefault(s => s.Id == query.ServiceId)
                      ?? throw new ServiceNotFoundException(query.ServiceId);

        return service.AsDetailsDto();
    }
}