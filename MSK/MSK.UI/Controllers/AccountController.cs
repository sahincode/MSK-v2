using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;

namespace MSK.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModelDto adminLoginViewModel)
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

            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public  async Task< IActionResult> Register(RegisterModelDto registerModelDto)
        {

            if (!ModelState.IsValid) return View(registerModelDto);

            try
            {
                await _accountService.Register(registerModelDto);
            }
            catch (InvalidUserCredentialException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            return RedirectToAction("login", "Account");
        }
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("login", "Account");
        }

    }
}
