using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Orders.Commands.CheckoutCartCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EGS.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OrderController : BaseApiController
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICurrentUserService _currentUserService;
        public OrderController(ISender mediator, ILogger<CartController> logger, ICurrentUserService currentUserService) : base(mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<ServiceResult<OrderDto>>> Checkout(CheckoutCartCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }


    }
}