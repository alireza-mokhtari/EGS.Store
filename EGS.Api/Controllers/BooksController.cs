using EGS.Application.Books.Commands.CreateBookCommand;
using EGS.Application.Books.Commands.DeleteBookCommand;
using EGS.Application.Books.Commands.UpdateBookCommand;
using EGS.Application.Books.Queries;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EGS.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BooksController : BaseApiController
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ISender mediator, ILogger<BooksController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<BookDto>>> Create(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResult<BookDto>>> Update(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{isbn}")]
        public async Task<ActionResult<ServiceResult<BookDto>>> Delete(string isbn, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteBookCommand { ISBN = isbn }, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{isbn}")]

        public async Task<ActionResult<ServiceResult<BookDto>>> GetByISBN(string isbn, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetBookQuery { ISBN = isbn }, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<BookDto>>>> GetList([FromQuery] GetPaginatedBooksQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}