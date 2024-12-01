using Application.DTOs;
using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infracstructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly Dictionary<string, string> _refreshTokens = new(); // Use DB in production.

        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public TokenResponse GenerateTokens(AppUser user)
        {
            var claims = new[]
             {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result.FirstOrDefault()) // Assign roles
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );
            var refreshToken = Guid.NewGuid().ToString();
            _refreshTokens[refreshToken] = user.Id.ToString();

            return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = refreshToken,
                Expiration = accessToken.ValidTo
            };
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            return _refreshTokens.ContainsKey(refreshToken);
        }

        public async Task RevokeRefreshToken(string refreshToken)
        {
            if (_refreshTokens.ContainsKey(refreshToken))
            {
                _refreshTokens.Remove(refreshToken);
            }
        }

        public string? GetUserIdFromRefreshToken(string refreshToken)
        {
            if (_refreshTokens.TryGetValue(refreshToken, out var userId))
            {
                return userId;
            }
            return null;
        }
    }
}
