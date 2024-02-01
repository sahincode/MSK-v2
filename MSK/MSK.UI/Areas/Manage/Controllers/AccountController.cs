using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;

namespace MSK.Areas.Manage.Controllers
{

    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountService _accountService;

        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAccountService accountService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginDto adminLoginViewModel)
        {
            if (!ModelState.IsValid) return View(adminLoginViewModel);

            try
            {
                await _accountService.Login(adminLoginViewModel);
            }
            catch (InvalidUserCredentialException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            return RedirectToAction("index", "chef");
        }


        //public async Task<IActionResult> CreateAdmin()
        //{
        //    User admin = new User()
        //    {
        //        FullName = "sahin ismayilov",
        //        UserName = "sakodiyo",
        //        Email = "sakode@gmail.com"
        //    };

        //    var result = await _userManager.CreateAsync(admin, "Sahin6134@");
        //    await _userManager.AddToRoleAsync(admin, "SuperAdmin");

        //    return Ok(result);
        //}
        //public async Task<IActionResult> CreateRoles()
        //{
        //    IdentityRole role1 = new IdentityRole("Admin");
        //    IdentityRole role2 = new IdentityRole("User");
        //    IdentityRole role3 = new IdentityRole("SuperAdmin");

        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);

        //    return Ok("Roles is Created");
        //}
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("login", "Account");
        }

    }
}
