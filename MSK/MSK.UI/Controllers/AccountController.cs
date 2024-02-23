using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MSK.Business.DTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.UI.ViewModels;
using System.Text;

namespace MSK.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;

        public AccountController(IAccountService accountService, 
            UserManager<User> userManager )
        {
            this._accountService = accountService;
            this._userManager = userManager;
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

            return RedirectToAction("index", "aisupport");
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModelDto registerModelDto)
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
            TempData["SuccessMessage"] = "Email sent successfully.";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("login", "Account");
        }
        public async Task<IActionResult> ConfirmEmail(string uid, string token)
        {
            ConfirmEmailViewModel confirmEmailViewModel = null;
            var user = await _userManager.FindByIdAsync(uid);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{uid}'.");
            }




            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token))



                    ;
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    confirmEmailViewModel = new ConfirmEmailViewModel()
                    {
                        EmailVerified = true
                    };
                }
            }
            return View(confirmEmailViewModel);
        }
    }
}
