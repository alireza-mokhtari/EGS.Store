using EGS.Application.Books.Queries;
using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Orders.Commands.CheckoutCartCommand;
using EGS.Application.Orders.Commands.ProcessOrderCommand;
using EGS.Application.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EGS.Api.Controllers
{
    [ApiController]
    public class OrderController : BaseApiController
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICurrentUserService _currentUserService;
        public OrderController(ISender mediator, ILogger<CartController> logger, ICurrentUserService currentUserService) : base(mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        [HttpPost(nameof(Checkout))]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<ServiceResult<OrderDto>>> Checkout(CheckoutCartCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost(nameof(Process))]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<OrderDto>>> Process(ProcessOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("my-orders")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<ServiceResult<PaginatedList<OrderSummaryDto>>>> GetMyOrders([FromQuery] PageableQuery options, CancellationToken cancellationToken)
        {
            var query = new CustomerOrdersQuery
            {
                CustomerId = _currentUserService.UserId,
                PageNumber = options.PageNumber,
                PageSize = options.PageSize
            };

            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("customer-orders")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<PaginatedList<OrderSummaryDto>>>> GetCustomerOrders([FromQuery] CustomerOrdersQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<PaginatedList<OrderSummaryDto>>>> GetList([FromQuery] GetPaginatedOrdersQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<PaginatedList<OrderSummaryDto>>>> GetList(long id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new OrderDetailsQuery { OrderId = id }, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}/history")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<List<OrderHistoryItemDto>>>> GetHistory(long id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new OrderHistoryQuery { OrderId = id }, cancellationToken);
            return Ok(result);
        }
    }
}