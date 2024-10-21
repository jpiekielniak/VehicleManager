namespace VehicleManager.Application.ServiceBooks.Queries.GetService.DTO;

public record ServiceDetailsDto(
    Guid Id,
    string Title,
    string Description,
    DateTimeOffset Date,
    List<CostDetailsDto> Costs
);