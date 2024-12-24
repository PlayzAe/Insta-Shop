using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using ECommerceAPI.Models;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // A simple in-memory user store 
        private static readonly List<User> Users = new List<User>
        {
            new User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password") }  // Hashed password example
        };

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = Users.FirstOrDefault(u => u.Username == model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username ?? "defaultUsername"), // Provide a default value if null
                new Claim(ClaimTypes.NameIdentifier, user.Username ?? "defaultUsername") // Provide a default value if null
            };

            var secretKey = _configuration["JwtSettings:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception("Secret key for JWT is missing in configuration.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiryDurationStr = _configuration["JwtSettings:ExpiryDuration"];
            if (string.IsNullOrEmpty(expiryDurationStr) || !int.TryParse(expiryDurationStr, out int expiryDuration))
            {
                throw new Exception("Invalid or missing expiry duration in JWT settings.");
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:ValidIssuer"],
                audience: _configuration["JwtSettings:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryDuration),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }

    public class LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class User
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
    }
}
