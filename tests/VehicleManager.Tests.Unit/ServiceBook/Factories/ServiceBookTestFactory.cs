using VehicleManager.Application.ServiceBooks.Commands.AddInspection;
using VehicleManager.Application.ServiceBooks.Commands.AddService;
using VehicleManager.Application.ServiceBooks.Commands.AddService.DTO;
using VehicleManager.Application.ServiceBooks.Commands.DeleteInspection;
using VehicleManager.Application.ServiceBooks.Commands.DeleteService;
using VehicleManager.Application.ServiceBooks.Queries.GetService;
using VehicleManager.Core.Vehicles.Builders;
using VehicleManager.Core.Vehicles.Entities;
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

    public DeleteInspectionCommand CreateDeleteInspectionCommand(Guid serviceBookId = default,
        Guid inspectionId = default)
        => new(
            serviceBookId == default ? Guid.NewGuid() : serviceBookId,
            inspectionId == default ? Guid.NewGuid() : inspectionId
        );

    public Inspection CreateInspection()
        => new InspectionBuilder()
            .WithTitle(_faker.Lorem.Paragraph())
            .WithInspectionType(_faker.Random.Enum<InspectionType>())
            .WithPerformDate(_faker.Date.Past())
            .WithScheduledDate(_faker.Date.Past())
            .Build();

    public DeleteServiceCommand CreateDeleteServiceCommand(Guid serviceBookId = default, Guid serviceId = default)
        => new(
            serviceBookId == default ? Guid.NewGuid() : serviceBookId,
            serviceId == default ? Guid.NewGuid() : serviceId
        );

    public Service CreateService()
        => new ServiceBuilder()
            .WithTitle(_faker.Lorem.Paragraph())
            .WithDescription(_faker.Lorem.Paragraph())
            .WithServiceDate(_faker.Date.Past())
            .Build();

    public GetServiceQuery GetServiceQuery(Guid serviceBookId = default, Guid serviceId = default)
        => new(
            serviceBookId == default ? Guid.NewGuid() : serviceBookId,
            serviceId == default ? Guid.NewGuid() : serviceId
        );
}