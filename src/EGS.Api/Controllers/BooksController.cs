using EGS.Application.Books.Commands.CreateBookCommand;
using EGS.Application.Books.Commands.DeleteBookCommand;
using EGS.Application.Books.Commands.UpdateBookCommand;
using EGS.Application.Books.Queries;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using EGS.Application.Inventory.Commands.AddStockCommand;
using EGS.Application.Inventory.Commands.ReduceStockCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EGS.Api.Controllers
{
    [ApiController]
    public class BooksController : BaseApiController
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ISender mediator, ILogger<BooksController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<BookDto>>> Create(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<BookDto>>> Update(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{isbn}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<BookDto>>> Delete(string isbn, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteBookCommand { ISBN = isbn }, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{isbn}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<BookDto>>> GetByISBN(string isbn, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetBookQuery { ISBN = isbn }, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<PaginatedList<BookDto>>>> GetList([FromQuery] GetPaginatedBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<ServiceResult<PaginatedList<BookDto>>>> Search([FromQuery] SearchBooksQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("add-stock")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<InventoryTransactionDto>>> AddStock([FromBody] AddStockCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("reduce-stock")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<InventoryTransactionDto>>> ReduceStock([FromBody] ReduceStockCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

    }
}