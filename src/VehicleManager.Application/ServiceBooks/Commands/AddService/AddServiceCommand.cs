using VehicleManager.Application.ServiceBooks.Commands.AddService.DTO;

namespace VehicleManager.Application.ServiceBooks.Commands.AddService;

internal record AddServiceCommand(
    string Title,
    string Description,
    DateTimeOffset Date,
    List<CostDto> Costs
) : IRequest<AddServiceResponse>
{
    internal Guid ServiceBookId { get; init; }
}