using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TrendSense.Domain;

namespace TrendSense.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        public UserManager<AppUser> _userManager;

        public RegisterUserCommandHandler(UserManager<AppUser> userManager) => _userManager = userManager;

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new ValidationException("Email is already in use");
            }

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return user.Id;
        }
    }
}
