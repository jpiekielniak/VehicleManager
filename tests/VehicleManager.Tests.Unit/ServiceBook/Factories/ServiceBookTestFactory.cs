using VehicleManager.Application.ServiceBooks.Commands.AddInspection;
using VehicleManager.Application.ServiceBooks.Commands.AddService;
using VehicleManager.Application.ServiceBooks.Commands.AddService.DTO;
using VehicleManager.Core.Vehicles.Entities.Enums;

namespace VehicleManager.Tests.Unit.ServiceBook.Factories;

internal class ServiceBookTestFactory
{
    private readonly Faker _faker = new();

    public AddInspectionCommand CreateAddInspectionCommand()
        => new(
            _faker.Lorem.Paragraph(),
            _faker.Date.Past(),
            _faker.Date.Past(),
            _faker.Random.Enum<InspectionType>()
        );

    public Core.Vehicles.Entities.ServiceBook CreateServiceBook()
        => Core.Vehicles.Entities.ServiceBook.Create();

    public AddServiceCommand CreateAddServiceCommand(Guid serviceBookId)
        => new(
            _faker.Lorem.Paragraph(),
            _faker.Lorem.Paragraph(),
            _faker.Date.Past(),
            [new CostDto(_faker.Lorem.Word(), _faker.Random.Decimal())]
        )
        {
            ServiceBookId = serviceBookId
        };
}