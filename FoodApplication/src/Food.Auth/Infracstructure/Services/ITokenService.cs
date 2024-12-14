using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructure.Services
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
    public interface ITokenService
    {
        TokenResponse GenerateTokens(AppUser user);
        bool ValidateRefreshToken(string refreshToken);
        Task RevokeRefreshToken(string refreshToken);
        string? GetUserIdFromRefreshToken(string refreshToken);
    }
}
