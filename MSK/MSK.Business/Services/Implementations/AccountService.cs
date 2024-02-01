using Microsoft.AspNetCore.Identity;
using MSK.Business.DTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;


namespace MSK.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task Login(AdminLoginDto adminLoginViewModel)
        {
            User admin = null;

            admin = await _userManager.FindByEmailAsync(adminLoginViewModel.Email);
            if (admin == null) throw new InvalidUserCredentialException("", "Username or password is wrong!");

            var result = await _signInManager.PasswordSignInAsync(admin, adminLoginViewModel.Password, false, false);

            if (!result.Succeeded) throw new InvalidUserCredentialException("", "Username or password is wrong!");
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
