using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarketAPI.Models;

namespace StockMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StockMarketContext _context;

        public UsersController(StockMarketContext context)
        {
            _context = context;
        }

        // POST: api/Users/Register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            // Kiểm tra username đã tồn tại chưa
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest(new { message = "Username đã tồn tại" });
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
        }

        // POST: api/Users/Login
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginModel login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == login.Username && u.Password == login.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Sai username hoặc password" });
            }

            return Ok(new { message = "Đăng nhập thành công", userId = user.Id, username = user.Username });
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }

    // Model dùng để nhận dữ liệu login từ Angular
    public class LoginModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}