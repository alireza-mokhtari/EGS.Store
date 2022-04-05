using EGS.Application.Common.Models;
using EGS.Application.Dto;

namespace EGS.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameByIdAsync(string userId);

        Task<string> GetUserNameAsync(string userId);

        Task<ApplicationUserDto> CheckUserPassword(string userName, string password);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string role);

        Task<bool> UserIsInRole(string userId, string role);

        Task<Result> DeleteUserAsync(string userId);
        Task<PaginatedList<ApplicationUserDto>> GetPaginatedListAsync(int pageSize, int pageNumber,
            CancellationToken cancellationToken);
        Task<IList<string>> GetRolesAsync(ApplicationUserDto user);
    }
}
