using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Application.ServiceBooks.Commands.DeleteService;

internal sealed class DeleteServiceCommandHandler(
    IServiceBookRepository serviceBookRepository
) : IRequestHandler<DeleteServiceCommand>
{
    public async Task Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
    {
        var serviceBook = await serviceBookRepository.GetAsync(command.ServiceBookId, cancellationToken)
                          ?? throw new ServiceBookNotFoundException(command.ServiceBookId);

        var service = serviceBook.Services.FirstOrDefault(s => s.Id == command.ServiceId)
                      ?? throw new ServiceNotFoundException(command.ServiceId);

        serviceBook.RemoveService(service);
        await serviceBookRepository.SaveChangesAsync(cancellationToken);
    }
}