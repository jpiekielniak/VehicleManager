using VehicleManager.Application.ServiceBooks.Queries.BrowseServices.DTO;
using VehicleManager.Core.Common.Pagination;

namespace VehicleManager.Application.ServiceBooks.Queries.BrowseServices;

public record BrowseServicesQuery(Guid ServiceBookId, SieveModel SieveModel) : IRequest<PaginationResult<ServiceDto>>;