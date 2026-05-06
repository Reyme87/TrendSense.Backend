using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrendSense.Application.Features.Auth.Commands.Login;
using TrendSense.Application.Interfaces;
using TrendSense.Domain;

namespace TrendSense.Application.Common.JwtTokens
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration) => _configuration = configuration;

        public AuthResultDto GenerateToken(AppUser user)
        {
            var key = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            var expires = DateTime.Now.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
                );

            return new AuthResultDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expires
            };
        }
    }
}
