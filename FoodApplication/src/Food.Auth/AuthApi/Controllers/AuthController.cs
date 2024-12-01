using Application.DTOs;
using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(UserManager<AppUser> _userManager, ITokenService _tokenService) : ControllerBase
    {
        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUserData()
        {
            return Ok("This is User data.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = registerRequest.Email,
                    Email = registerRequest.Email,
                    CreatedBy = "admin",
                    CreatedOn = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, registerRequest.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    var tokens = _tokenService.GenerateTokens(user);
                    return Ok(tokens);
                }
                return BadRequest(new { Message = "User creation failed", Errors = result.Errors.Select(e => e.Description) });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }
            var tokens = _tokenService.GenerateTokens(user);
            return Ok(tokens);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (!_tokenService.ValidateRefreshToken(refreshToken))
            {

                return Unauthorized(new { Message = "Invalid refresh token." });
            }

            var userId = _tokenService.GetUserIdFromRefreshToken(refreshToken);
            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid refresh token." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized(new { Message = "User not found." });
            }

            var tokens = _tokenService.GenerateTokens(user);
            await _tokenService.RevokeRefreshToken(refreshToken);

            return Ok(tokens);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
        {
            await _tokenService.RevokeRefreshToken(refreshToken);
            return Ok(new { Message = "Refresh token revoked successfully." });
        }
    }
}
