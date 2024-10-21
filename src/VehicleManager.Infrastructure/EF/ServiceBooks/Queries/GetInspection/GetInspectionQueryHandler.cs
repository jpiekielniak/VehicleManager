using VehicleManager.Application.ServiceBooks.Queries.GetInspection;
using VehicleManager.Application.ServiceBooks.Queries.GetInspection.DTO;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Infrastructure.EF.ServiceBooks.Queries.GetInspection;

internal sealed class GetInspectionQueryHandler(
    IServiceBookRepository serviceBookRepository
) : IRequestHandler<GetInspectionQuery, InspectionDetailsDto>
{
    public async Task<InspectionDetailsDto> Handle(GetInspectionQuery query,
        CancellationToken cancellationToken)
    {
        var serviceBook = await serviceBookRepository.GetAsync(query.ServiceBookId, cancellationToken, true)
                          ?? throw new ServiceBookNotFoundException(query.ServiceBookId);

        var inspection = serviceBook.Inspections.FirstOrDefault(i => i.Id == query.InspectionId)
                         ?? throw new InspectionNotFoundException(query.InspectionId);

        return inspection.AsDetailsDto();
    }
}