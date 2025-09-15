using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using OnuInstitute.Data;
using OnuInstitute.Models;
using OnuInstitute.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using static System.Object;

namespace OnuInstitute.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IAccount _account;

        public AccountController(UserManager<IdentityUser> userManager, IAccount account, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _account = account;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var name = new IdentityUser { UserName = model.Username };
            var result = await _userManager.CreateAsync(name, model.Password);

            //var result2 = await _account.RegisterAsync

            if (result.Succeeded)
            {
                return Ok(new { message = "User Registered Successful" });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaim = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                authClaim.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
                    claims: authClaim,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!)),
                    SecurityAlgorithms.HmacSha256
                    )
                    );
                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized();
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role));
                if (result.Succeeded)
                {
                    return Ok(new { message = "Role Added Successfully" });
                }
                return BadRequest(result.Errors);

            }
            return BadRequest("Role already Exists");
        }


        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRole model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role assigned successfully" });
            }
            return BadRequest(result.Errors);
        }
    }
}
