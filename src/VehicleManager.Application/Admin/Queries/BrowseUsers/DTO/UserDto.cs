namespace VehicleManager.Application.Admin.Queries.BrowseUsers.DTO;

public record UserDto(
    Guid Id,
    string Email,
    string Role,
    DateTimeOffset CreatedAt
);