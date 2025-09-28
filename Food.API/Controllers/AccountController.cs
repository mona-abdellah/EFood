using Food.DTO.Customer;
using Food.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Food.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Customer> userManager;
        private readonly SignInManager<Customer> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> identityRole;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<Customer> _userManager
            ,SignInManager<Customer> _signInManager,RoleManager<IdentityRole<Guid>> _identityRole,IConfiguration _configuration)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            identityRole = _identityRole;
            configuration = _configuration;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(CreateORUpdateCustomerDTO customerDTO)
        {
            var user = await userManager.FindByEmailAsync(customerDTO.Email);

            if (user == null && !string.IsNullOrEmpty(customerDTO.Username))
            {
                user = await userManager.FindByNameAsync(customerDTO.Username);
            }

            if (user == null)
            {
                return Unauthorized("Wrong Email or Username");
            }
            var check = await userManager.CheckPasswordAsync(user, customerDTO.Password);
                if (!check)
                {
                    return Unauthorized("Wrong Password");
                }
                List<Claim> claims = new List<Claim>()
                {
                       new Claim(ClaimTypes.Name,customerDTO.Username),
                       new Claim(ClaimTypes.Email, customerDTO.Email)
                };
                var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"]));
                var token = new JwtSecurityToken(
                    issuer: configuration["jwt:issuer"],
                    audience: configuration["jwt:audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
                 );
                var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(tokenStr);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateORUpdateCustomerDTO customerDTO)
        {
            var email = await userManager.FindByEmailAsync(customerDTO.Email);
            if (email != null)
            {
                return StatusCode(500, "This Email Already Exists");
            }
            var user = new Customer { 
                UserName = customerDTO.Username,
                FName=customerDTO.FName,
                Lname=customerDTO.Lname,
                Email = customerDTO.Email
            };
            var res = await userManager.CreateAsync(user,customerDTO.Password);
            if (res.Succeeded)
            {
                return Ok("Created Successfully");
            }
            return StatusCode(500, res.Errors);
        }

    }
}
