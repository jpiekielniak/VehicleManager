namespace CarManagement.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;

public record UserDetailsDto(
    Guid Id,
    string Email,
    string UserName,
    string PhoneNumber,
    DateTimeOffset CreatedAt
);