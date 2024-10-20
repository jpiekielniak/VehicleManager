using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Application.ServiceBooks.Commands.AddInspection;

internal sealed class AddInspectionCommandHandler(
    IServiceBookRepository serviceBookRepository
) : IRequestHandler<AddInspectionCommand, AddInspectionResponse>
{
    public async Task<AddInspectionResponse> Handle(AddInspectionCommand command,
        CancellationToken cancellationToken)
    {
        var serviceBook = await serviceBookRepository.GetAsync(command.ServiceBookId, cancellationToken)
                          ?? throw new ServiceBookNotFoundException(command.ServiceBookId);

        var inspection = new InspectionBuilder()
            .WithTitle(command.Title)
            .WithPerformDate(command.PerformDate)
            .WithScheduledDate(command.ScheduledDate)
            .WithInspectionType(command.InspectionType)
            .WithServiceBook(serviceBook)
            .Build();

        serviceBook.AddInspection(inspection);

        await serviceBookRepository.UpdateAsync(serviceBook, cancellationToken);
        await serviceBookRepository.SaveChangesAsync(cancellationToken);

        return new AddInspectionResponse(inspection.Id);
    }
}