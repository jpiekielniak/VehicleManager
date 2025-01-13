using VehicleManager.Application.ServiceBooks.Commands.AddInspection;
using VehicleManager.Application.ServiceBooks.Commands.AddService;
using VehicleManager.Application.ServiceBooks.Commands.AddService.DTO;
using VehicleManager.Application.ServiceBooks.Commands.DeleteInspection;
using VehicleManager.Application.ServiceBooks.Commands.DeleteService;
using VehicleManager.Application.ServiceBooks.Queries.GetInspection;
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
        )
    {
        ServiceBookId = Guid.NewGuid()
    };

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
            serviceBookId == Guid.Empty ? Guid.NewGuid() : serviceBookId,
            inspectionId == Guid.Empty ? Guid.NewGuid() : inspectionId
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
            serviceBookId == Guid.Empty ? Guid.NewGuid() : serviceBookId,
            serviceId == Guid.Empty ? Guid.NewGuid() : serviceId
        );

    public Service CreateService()
        => new ServiceBuilder()
            .WithTitle(_faker.Lorem.Paragraph())
            .WithDescription(_faker.Lorem.Paragraph())
            .WithServiceDate(_faker.Date.Past())
            .Build();

    public GetServiceQuery GetServiceQuery(Guid serviceBookId = default, Guid serviceId = default)
        => new(
            serviceBookId == Guid.Empty ? Guid.NewGuid() : serviceBookId,
            serviceId == Guid.Empty ? Guid.NewGuid() : serviceId
        );

    public GetInspectionQuery GetInspectionQuery(Guid serviceBookId = default, Guid inspectionId = default)
        => new(
            serviceBookId == Guid.Empty ? Guid.NewGuid() : serviceBookId,
            inspectionId == Guid.Empty ? Guid.NewGuid() : inspectionId
        );

    private List<CostDto> CreateValidCosts(int count = 2)
        => Enumerable.Range(0, count)
            .Select(_ => new CostDto(_faker.Lorem.Word(), _faker.Random.Decimal(1, 1000)))
            .ToList();

    public List<CostDto> CreateCostsWithNegativeAmount()
        =>
        [
            new(_faker.Lorem.Word(), _faker.Random.Decimal(1, 1000)),
            new(_faker.Lorem.Word(), -_faker.Random.Decimal(1, 1000))
        ];

    public List<CostDto> CreateCostsWithZeroAmount()
        =>
        [
            new(_faker.Lorem.Word(), _faker.Random.Decimal(1, 1000)),
            new(_faker.Lorem.Word(), 0)
        ];

    public List<CostDto> CreateCostsWithNullTitle()
        =>
        [
            new(_faker.Lorem.Word(), _faker.Random.Decimal(1, 1000)),
            new(null!, _faker.Random.Decimal(1, 1000))
        ];

    public AddServiceCommand CreateAddServiceCommand()
        => new(
            _faker.Lorem.Paragraph(),
            _faker.Lorem.Paragraph(),
            _faker.Date.Past(),
            CreateValidCosts()
        )
        {
            ServiceBookId = Guid.NewGuid()
        };

    public AddServiceCommand CreateAddServiceCommandWithEmptyCosts()
        => new(
            _faker.Lorem.Word(),
            _faker.Lorem.Paragraph(),
            _faker.Date.Past(),
            []
        )
        {
            ServiceBookId = Guid.NewGuid()
        };

    public AddServiceCommand CreateAddServiceCommandWithNullCosts()
        => new(
            _faker.Lorem.Word(),
            _faker.Lorem.Paragraph(),
            _faker.Date.Past(),
            null!
        )
        {
            ServiceBookId = Guid.NewGuid()
        };
}