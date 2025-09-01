using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantRESTAPI.Data;
using ResturantRESTAPI.Models;
using ResturantRESTAPI.Utility;

namespace ResturantRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ResturantDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(ResturantDbContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        [HttpPost("Admin Login")]
        public async Task<IActionResult> Login(Admin admin)
        {
            var validAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == admin.Username);
            if (validAdmin == null)
                return Unauthorized("Invalid Info");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(admin.PasswordHash, validAdmin.PasswordHash);
            if (!isPasswordValid)
                return Unauthorized("Invalid Info");

            var token = JwtTokenUtility.GenerateToken(validAdmin, _configuration);

            return Ok(new { token });
        }

        [HttpPost("Admin Register")]
        public async Task<IActionResult> Register(Admin admnin)
        {
            var existingAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == "admin");
            if (existingAdmin != null)
            {
                return BadRequest("Admin user already exists.");
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(admnin.PasswordHash);
            admnin.PasswordHash = hashedPassword;

            _context.Admins.Add(admnin);
            await _context.SaveChangesAsync();
            return Created();
        }
    }
}
