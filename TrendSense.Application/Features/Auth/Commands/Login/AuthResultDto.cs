namespace TrendSense.Application.Features.Auth.Commands.Login
{
    public class AuthResultDto
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
