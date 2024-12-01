using Application.DTOs;
using Domain.Entity;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        TokenResponse GenerateTokens(AppUser user);
        bool ValidateRefreshToken(string refreshToken);
        Task RevokeRefreshToken(string refreshToken);
        string? GetUserIdFromRefreshToken(string refreshToken);
    }
}
