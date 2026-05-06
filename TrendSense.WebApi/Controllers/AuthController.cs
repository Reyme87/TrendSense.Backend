using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrendSense.Application.Features.Auth.Commands.Login;
using TrendSense.Application.Features.Auth.Commands.Register;

namespace TrendSense.WebApi.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator) { }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register(RegisterUserCommand command)
        {
            var userId = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResultDto>> Login(LoginUserCommand command)
        {
            var authResult = await Mediator.Send(command);
            return Ok(authResult);
        }
    }
}
