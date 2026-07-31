using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrendSense.Application.Features.Auth.Commands.Login;
using TrendSense.Application.Features.Auth.Commands.Register;

namespace TrendSense.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <remarks>
        /// Creates a new user account
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Returns the created user's id</returns>
        /// <response code="200">User successfully registered</response>
        /// <response code="400">Invalid registration data</response>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Register(RegisterUserCommand command)
        {
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }

        /// <summary>
        /// Provides a login for a created user
        /// </summary>
        /// <remarks>
        /// Authorizes an already created user
        /// </remarks>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200">User successfully logged in</response>
        /// <response code="401">Invalid username or password</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthResultDto>> Login(LoginUserCommand command)
        {
            var authResult = await Mediator.Send(command);
            return Ok(authResult);
        }
    }
}
