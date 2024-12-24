using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using ECommerceAPI.Models;
using System.Linq;

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

        // Login endpoint
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
        new Claim(ClaimTypes.Name, user.Username ?? "defaultUsername"),
        new Claim(ClaimTypes.NameIdentifier, user.Username ?? "defaultUsername")
    };

            var secretKey = _configuration["JwtSettings:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception("Secret key for JWT is missing in configuration.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Remove the expiry duration logic since you don't want the token to expire
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:ValidIssuer"],
                audience: _configuration["JwtSettings:ValidAudience"],
                claims: claims,
                expires: null,  // Token does not expire
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        // Register endpoint
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // Check if the user already exists
            if (Users.Any(u => u.Username == model.Username))
            {
                return Conflict(new { Message = "Username already exists" });
            }

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Add the new user
            var newUser = new User
            {
                Username = model.Username,
                PasswordHash = hashedPassword
            };

            Users.Add(newUser);

            return CreatedAtAction(nameof(Login), new { username = model.Username }, new { Message = "User registered successfully" });
        }
    }

    // Login model
    public class LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    // Register model
    public class RegisterModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    // User model
    public class User
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
    }
}
