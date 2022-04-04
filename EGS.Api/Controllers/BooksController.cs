using EGS.Application.Books.Commands.CreateBookCommand;
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

        [HttpGet(nameof(GetAll))]
        [Authorize(Roles = "Customer")]
        public IActionResult GetAll()
        {
            return Ok(Summaries);
        }
    }
}