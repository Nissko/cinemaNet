using Cinema.Application.Application.Templates.AuthModels;
using Cinema.Application.Common.Interfaces;
using Cinema.Domain.Aggregates.Users;
using Cinema.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly CinemaDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(CinemaDbContext context, IAuthService authService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return Unauthorized("Неверные логин или пароль");
            }

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token, user.Email });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _context.User.AnyAsync(u => u.Email == model.Email))
            {
                return BadRequest("Пользователь с таким email уже существует");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            
            var user = new UserEntity(model.Email, passwordHash);

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok("Регистрация успешна");
        }
    }
}