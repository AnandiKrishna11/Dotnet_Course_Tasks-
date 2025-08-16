using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly StudentDataBaseContext _context;

        public AuthController(IConfiguration config, StudentDataBaseContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
           
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == login.Username);

            if (user == null || user.Password != login.Password) 
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var token = GenerateJwtToken(user.Username);
            return Ok(new { token });
        }

        private string GenerateJwtToken(string username)
        {
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]!);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}