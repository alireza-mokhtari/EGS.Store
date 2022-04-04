using EGS.Application.Books.Commands.CreateBookCommand;
using EGS.Application.Books.Commands.DeleteBookCommand;
using EGS.Application.Books.Commands.UpdateBookCommand;
using EGS.Application.Common.Models;
using EGS.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EGS.Api.Controllers
{
    [ApiController]
    public class BooksController : BaseApiController
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<BooksController> _logger;

        public BooksController(ISender mediator, ILogger<BooksController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpPost(nameof(Create))]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<BookDto>>> Create(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut(nameof(Update))]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<BookDto>>> Update(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResult<BookDto>>> Delete(long id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DeleteBookCommand { Id = id }, cancellationToken);
            return Ok(result);
        }

        [HttpGet(nameof(GetAll))]
        [Authorize(Roles = "Customer")]
        public IActionResult GetAll()
        {
            return Ok(Summaries);
        }
    }
}