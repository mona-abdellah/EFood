using AutoMapper;
using Food.DTO.Customer;
using Food.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Food.Presentaion.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Customer> userManager;
        private readonly SignInManager<Customer> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        public AccountController(UserManager<Customer> _userManager,SignInManager<Customer> _signInManager,
            IWebHostEnvironment _webHostEnvironment,IMapper _mapper,RoleManager<IdentityRole<Guid>> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            webHostEnvironment = _webHostEnvironment;
            mapper = _mapper;
            roleManager = _roleManager;
        }
        [Authorize(Roles ="admin")]
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> AddAdmin(AdminDTO adminDTO)
        {
            if (ModelState.IsValid)
            {
                var admin = mapper.Map<Customer>(adminDTO);
                var res = await userManager.CreateAsync(admin, adminDTO.Password);
                if (res.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync("admin"))
                    {
                        await roleManager.CreateAsync(new IdentityRole<Guid>("admin"));
                    }
                    await userManager.AddToRoleAsync(admin, "admin");

                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(adminDTO);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginDTO adminLoginDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emal=adminLoginDTO.Username;
                    var user = await userManager.FindByNameAsync(adminLoginDTO.Username);
                    if (user != null)
                    {
                        var check = await userManager.CheckPasswordAsync(user, adminLoginDTO.Password);
                        var role = await userManager.GetRolesAsync(user);
                        if (check == true && role.Contains("admin"))
                        {
                            await signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("GetAllCategory", "Category");
                        }
                    }

                }
                ModelState.AddModelError("", "Invalid Login");
                return RedirectToAction("Login");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error");
                return View(adminLoginDTO);
            }       
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
