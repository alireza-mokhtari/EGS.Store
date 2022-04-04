using EGS.Application.Common.Exceptions;
using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EGS.Infrastructure.Identity;
using MapsterMapper;
using Mapster;
using EGS.Infrastructure.Extensions;

namespace EGS.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public IdentityService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<string> GetUserNameByIdAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.UserName;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new UnauthorizeException();
            }

            return user.UserName;
        }

        public async Task<ApplicationUserDto> CheckUserPassword(string email, string password)
        {
            ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))            
                return _mapper.Map<ApplicationUserDto>(user);            

            return null;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string role)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var addedUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userName);
                await _userManager.AddToRoleAsync(addedUser, role);
            }
            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> UserIsInRole(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<PaginatedList<ApplicationUserDto>> GetPaginatedListAsync
            (int pageSize, int pageNumber, CancellationToken cancellationToken)
        {

            return await _userManager.Users
                .ProjectToType<ApplicationUserDto>(_mapper.Config)
                .PaginatedListAsync(pageNumber, pageSize, cancellationToken);

        }

        public Task<IList<string>> GetRolesAsync(ApplicationUserDto user)
        {
            return _userManager.GetRolesAsync(_mapper.Map<ApplicationUser>(user));
        }
    }
}
