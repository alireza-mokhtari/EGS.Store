using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;

namespace EGS.Application.Users.Queries.GetUsers
{
    public class GetPaginatedUsersQuery : PageableQuery, IRequestWrapper<PaginatedList<ApplicationUserDto>>
    {
        
    }

    public class GetPaginatedVisitorsQueryHandler 
        : IRequestHandlerWrapper<GetPaginatedUsersQuery, PaginatedList<ApplicationUserDto>>
    {
        private readonly IIdentityService _identityService;

        public GetPaginatedVisitorsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ServiceResult<PaginatedList<ApplicationUserDto>>> Handle(GetPaginatedUsersQuery request
            , CancellationToken cancellationToken)
        {
            var result = await _identityService
                .GetPaginatedListAsync(request.PageSize, request.PageNumber, cancellationToken);
            return result.TotalCount > 0 
                ? ServiceResult.Success(result) 
                : ServiceResult.Failed<PaginatedList<ApplicationUserDto>>(ServiceError.NotFound);
        }
    }
}
