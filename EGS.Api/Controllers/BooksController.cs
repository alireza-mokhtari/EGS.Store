using Microsoft.AspNetCore.Mvc;

namespace EGS.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }        
    }
}