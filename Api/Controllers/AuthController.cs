using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Application.Requests;
using MoviesAPI.Domain.Entities;

namespace MoviesAPI.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ExtendedRegisterRequest model)
        {
            var user = new AppUser { UserName = model.Username, Email = model.RegisterRequest.Email };
            var result = await _userManager.CreateAsync(user, model.RegisterRequest.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid credentials");

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new { accessToken, refreshToken });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest model)
        {
            if (model == null || string.IsNullOrEmpty(model.RefreshToken))
                return BadRequest("Invalid request");

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == model.RefreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token");

            var newAccessToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken });
        }

        private string GenerateJwtToken(AppUser user)
        {
            var keyString = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(keyString))
            {
                throw new InvalidOperationException("JWT Key is not configured.");
            }

            var key = Encoding.UTF8.GetBytes(keyString);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }
    }
}
