namespace VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;

public record UserDetailsDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Role,
    DateTimeOffset CreatedAt
);