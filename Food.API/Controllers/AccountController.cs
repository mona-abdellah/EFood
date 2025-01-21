using Food.DTO.Customer;
using Food.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Food.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Customer> userManager;
        private readonly SignInManager<Customer> signInManager;
        private readonly IdentityRole<Guid> identityRole;
        public AccountController(UserManager<Customer> _userManager
            ,SignInManager<Customer> _signInManager,IdentityRole<Guid> _identityRole)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            identityRole = _identityRole;
        }
        public async Task<IActionResult> Login(CreateORUpdateCustomerDTO customerDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(customerDTO.Email);
                if (user != null)
                {
                    var checkPass = await userManager.CheckPasswordAsync(user, customerDTO.Password);
                    if (checkPass)
                    {
                        await signInManager.SignInAsync(user, true);

                    }
                }
            }
        }


    }
}
