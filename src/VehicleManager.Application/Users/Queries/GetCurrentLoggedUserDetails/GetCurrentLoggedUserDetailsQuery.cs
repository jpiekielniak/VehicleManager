using VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails.DTO;

namespace VehicleManager.Application.Users.Queries.GetCurrentLoggedUserDetails;

public record GetCurrentLoggedUserDetailsQuery : IRequest<UserDetailsDto>;