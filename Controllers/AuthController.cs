using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.DTO;
using ToDoApi.Data;
using ToDoApi.Models;


namespace ToDoApi.Controllers
{
    public class AuthController:ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register (RegisterDTO dto)
        {
            var userExists = await _context.Users
                .AnyAsync(x => x.UserName == dto.UserName);

            if (userExists) return BadRequest("User already exists ");

            var user = new User
            {
                UserName = dto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok("User Registered");
        }


    }
}
