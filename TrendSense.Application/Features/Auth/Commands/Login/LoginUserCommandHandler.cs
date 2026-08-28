using MediatR;
using Microsoft.AspNetCore.Identity;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResultDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenGenerator _jwt;

        public LoginUserCommandHandler(UserManager<AppUser> userManager, IJwtTokenGenerator jwt) =>
            (_userManager, _jwt) = (userManager, jwt);

        public async Task<AuthResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var valid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!valid)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = _jwt.GenerateToken(user);

            return token;
        }
    }
}
