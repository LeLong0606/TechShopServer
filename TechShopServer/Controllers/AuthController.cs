using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechShopServer.Data;

using TechShopServer.Models;

namespace TechShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();

        private readonly IConfiguration _configuration;
        private readonly ApiDbContext _context;

        public AuthController(IConfiguration configuration, ApiDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }

            // Tạo đối tượng User
            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                // Bạn có thể thêm các thuộc tính khác nếu cần
            };

            try
            {
                // Thêm đối tượng User vào cơ sở dữ liệu
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(user); // Trả về đối tượng User vừa được tạo
            }
            catch (Exception ex)
            {
                // Bắt lỗi và trả về thông báo lỗi rõ ràng
                return StatusCode(500, $"Đã xảy ra lỗi khi đăng ký: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public ActionResult<User> login(UserDTO request)
        {
            if (user.UserName != request.UserName)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
