using EGS.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EGS.Api.Controllers
{
    /// <summary>
    /// Base api controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private ISender _mediator;
        private string _currentUserId;

        public BaseApiController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Mediator sender
        /// </summary>
        protected ISender Mediator => _mediator;
        
        /// <summary>
        /// Returns current user id if logged in
        /// </summary>
        protected string CurrentUserId => _currentUserId ??= HttpContext.RequestServices.GetService<ICurrentUserService>()?.UserId;
    }
}