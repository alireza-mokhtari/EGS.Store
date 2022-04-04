using EGS.Application.Users.Commands.Create;
using EGS.Application.Users.Queries.GetToken;
using EGS.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EGS.Api.Controllers
{
    [ApiController]
    public class AccountsController : BaseApiController
    {

        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ISender mediator, 
            ILogger<AccountsController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Login(GetTokenQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost(nameof(Register))]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Register(RegisterCustomerCommand query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }

    }
}