using EGS.Api.Models;
using EGS.Application.Common.Interfaces;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.ShoppingCart.Commands.AddToCartCommand;
using EGS.Application.ShoppingCart.Commands.DeleteCartItemCommand;
using EGS.Application.ShoppingCart.Commands.UpdateCartItemCommand;
using EGS.Application.ShoppingCart.Queries.GetCartQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EGS.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CartController : BaseApiController
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICurrentUserService _currentUserService;
        public CartController(ISender mediator, ILogger<CartController> logger, ICurrentUserService currentUserService) : base(mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<ShoppingCartItemDto>>> AddToCart(AddToCartModel model, CancellationToken cancellationToken)
        {
            var command = new AddToCartCommand();
            command.CustomerId = _currentUserService.UserId;
            command.Quantity = model.Quantity;
            command.BookId = model.BookId;

            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResult<ShoppingCartItemDto>>> UpdateQuantity(UpdateCartItemCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<ShoppingCartItemDto>>> DeleteCartItem(long id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteCartItemCommand { BookId = id }, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<ShoppingCartItemDto>>>> GetCartItems(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetCartQuery { CustomerId = _currentUserService.UserId }, cancellationToken);
            return Ok(result);
        }

    }
}