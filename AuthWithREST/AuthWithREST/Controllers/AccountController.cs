using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthWithREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (userName == "turkay" && password == "123")
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bu-çok-gizli-bir-cümle-ona-göre"));
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var claims = new[] { new Claim(JwtRegisteredClaimNames.Email, "a@b.com") };
                var token = new JwtSecurityToken(
                     issuer: "server",
                     audience: "client",
                     claims: claims,
                     notBefore: DateTime.Now,
                     expires: DateTime.Now.AddDays(1),
                     signingCredentials: credential
                    );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest();
        }
    }
}
