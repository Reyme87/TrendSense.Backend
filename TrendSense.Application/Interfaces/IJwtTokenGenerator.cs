using TrendSense.Application.Features.Auth.Commands.Login;
using TrendSense.Domain;

namespace TrendSense.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public AuthResultDto GenerateToken(AppUser user);
    }
}
