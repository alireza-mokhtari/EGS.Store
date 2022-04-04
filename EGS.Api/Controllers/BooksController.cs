using EGS.Api.Controllers;
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

        [HttpGet(nameof(GetAll))]
        [Authorize(Roles = "Customer")]
        public IActionResult GetAll()
        {
            return Ok(Summaries);
        }
    }
}