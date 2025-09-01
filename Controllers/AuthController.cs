using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantRESTAPI.Data;
using ResturantRESTAPI.DTOs;
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
        public async Task<IActionResult> Login(AdminDTO admin)
        {
            var validAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == admin.Username);
            if (validAdmin == null)
                return Unauthorized("Invalid Info");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(admin.PasswordHashed, validAdmin.PasswordHash);
            if (!isPasswordValid)
                return Unauthorized("Invalid Info");

            var token = JwtTokenUtility.GenerateToken(validAdmin, _configuration);

            return Ok(new { token });
        }

        [HttpPost("Admin Register")]
        public async Task<IActionResult> Register(AdminDTO admnin)
        {
            var existingAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == "admin");
            if (existingAdmin != null)
            {
                return BadRequest("Admin user already exists.");
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(admnin.PasswordHashed);
            admnin.PasswordHashed = hashedPassword;

            var convertToAdmin = new Admin
            {
                Username = admnin.Username,
                PasswordHash = admnin.PasswordHashed
            };

            _context.Admins.Add(convertToAdmin);
            await _context.SaveChangesAsync();
            return Created();
        }
    }
}
