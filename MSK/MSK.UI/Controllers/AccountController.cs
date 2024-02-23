using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MSK.Business.DTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.UI.ViewModels;
using NuGet.Common;
using System.Text;

namespace MSK.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IAccountService accountService,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._accountService = accountService;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginModelDto loginModelDto = new LoginModelDto()
            {

                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(loginModelDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModelDto adminLoginViewModel)
        {
            adminLoginViewModel.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid) return View(adminLoginViewModel);

            try
            {
                await _accountService.Login(adminLoginViewModel);
            }
            catch (InvalidUserCredentialException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View(adminLoginViewModel);
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
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto passwordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(passwordDto.Email);
                if (user is not null)
                {
                    await _accountService.GenerateForgetPasswordTokenAsync(user);
                }
                ModelState.Clear();
                passwordDto.EmailSent = true;
            }
            return View(passwordDto);
        }
        [AllowAnonymous, HttpGet]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPasswordDto resetPasswordDto = new ResetPasswordDto
            {
                Token = token,
                UserId = uid
            };
            return View(resetPasswordDto);
        }
        [AllowAnonymous, HttpPost]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (ModelState.IsValid)
            {
                resetPasswordDto.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDto.Token));
                var result = await _accountService.ResetPasswordAsync(resetPasswordDto);
                if (result.Succeeded)
                {
                    ModelState.Clear();

                    resetPasswordDto.Succeeded = true;
                    return View(resetPasswordDto);

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(resetPasswordDto);
        }
    }
}
