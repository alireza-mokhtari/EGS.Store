using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Domain.Security;

namespace EGS.Application.Users.Commands.Create
{
    public class RegisterCustomerCommand : IRequestWrapper<string>
    {        
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }

    public class RegisterCustomerCommandHandler : IRequestHandlerWrapper<RegisterCustomerCommand, string>
    {
        private readonly IIdentityService _identityService;

        public RegisterCustomerCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ServiceResult<string>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {

            var (result, userId) = await _identityService
                .CreateUserAsync(request.Email, request.Password, Constants.CUSTOMER_ROLE);
            
            return result.Succeeded 
                ? ServiceResult.Success(userId) 
                : ServiceResult.Failed<string>(string.Join('\n' , result.Errors) , ServiceError.UserFailedToCreate);
        }
    }
}
