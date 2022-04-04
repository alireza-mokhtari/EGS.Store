using EGS.Application.Dto;

namespace EGS.Application.Users.Queries.GetToken
{
    public class LoginResponse
    {
        public ApplicationUserDto User { get; set; }

        public string Token { get; set; }
    }
}
