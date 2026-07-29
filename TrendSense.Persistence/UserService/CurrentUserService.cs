using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrendSense.Application.Interfaces;

namespace TrendSense.Application.Common.UserService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public Guid UserId
        {
            get
            {
                var rawId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (Guid.TryParse(rawId, out var userId))
                {
                    return userId;
                }

                return Guid.Empty;
            }
        }

        public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
