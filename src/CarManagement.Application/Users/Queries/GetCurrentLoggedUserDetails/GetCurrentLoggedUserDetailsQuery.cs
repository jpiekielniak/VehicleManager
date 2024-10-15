using CarManagement.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;

namespace CarManagement.Application.Users.Queries.GetCurrentLoggedUserDetails;

public record GetCurrentLoggedUserDetailsQuery : IRequest<UserDetailsDto>;