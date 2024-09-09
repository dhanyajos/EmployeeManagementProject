using EmployeeProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeProject.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLogin model)
        {
            // Mock credentials for users and admins for simplicity
            var isAdmin = model.Username == "admin" && model.Password == "adminpassword";
            var isUser = model.Username == "user" && model.Password == "userpassword";

            if (!isAdmin && !isUser)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("user_and_admin_crud_operations_test");

            // Claims based on role
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, model.Username),
    };

            // Assign role-based claims
            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else if (isUser)
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
          
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                Role = isAdmin ? "Admin" : "User"
            });
        }
    }
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }


}
