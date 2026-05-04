using MediatR;

namespace TrendSense.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
