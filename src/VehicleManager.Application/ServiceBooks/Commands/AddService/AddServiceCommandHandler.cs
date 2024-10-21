using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
using VehicleManager.Core.Vehicles.Exceptions.ServiceBooks;
using VehicleManager.Core.Vehicles.Repositories;

namespace VehicleManager.Application.ServiceBooks.Commands.AddService;

internal sealed class AddServiceCommandHandler(
    IServiceBookRepository serviceBookRepository
) : IRequestHandler<AddServiceCommand, AddServiceResponse>
{
    public async Task<AddServiceResponse> Handle(AddServiceCommand command, CancellationToken cancellationToken)
    {
        var serviceBook = await serviceBookRepository.GetAsync(command.ServiceBookId, cancellationToken)
                          ?? throw new ServiceBookNotFoundException(command.ServiceBookId);

        var service = new ServiceBuilder()
            .WithTitle(command.Title)
            .WithDescription(command.Description)
            .WithServiceDate(command.Date)
            .WithServiceBook(serviceBook)
            .Build();

        var costs = command.Costs
            .Select(c => Cost.Create(c.Title, c.Amount, service.Id))
            .ToList();

        service.AddRangeCosts(costs);
        serviceBook.AddService(service);

        await serviceBookRepository.UpdateAsync(serviceBook, cancellationToken);
        await serviceBookRepository.SaveChangesAsync(cancellationToken);

        return new AddServiceResponse(service.Id);
    }
}