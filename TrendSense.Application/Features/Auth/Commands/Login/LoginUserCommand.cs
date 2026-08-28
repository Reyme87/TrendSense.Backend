using MediatR;

namespace TrendSense.Application.Features.Auth.Commands.Login
{
    public class LoginUserCommand : IRequest<AuthResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
