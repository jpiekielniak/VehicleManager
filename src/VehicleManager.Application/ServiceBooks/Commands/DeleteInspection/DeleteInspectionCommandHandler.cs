using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Application.ServiceBooks.Commands.DeleteInspection;

internal sealed class DeleteInspectionCommandHandler(
    IServiceBookRepository serviceBookRepository
) : IRequestHandler<DeleteInspectionCommand>
{
    public async Task Handle(DeleteInspectionCommand command, CancellationToken cancellationToken)
    {
        var serviceBook = await serviceBookRepository.GetAsync(command.ServiceBookId, cancellationToken)
                          ?? throw new ServiceBookNotFoundException(command.ServiceBookId);

        var inspection = serviceBook.Inspections.FirstOrDefault(i => i.Id == command.InspectionId)
                         ?? throw new InspectionNotFoundException(command.InspectionId);

        serviceBook.RemoveInspection(inspection);
        await serviceBookRepository.SaveChangesAsync(cancellationToken);
    }
}