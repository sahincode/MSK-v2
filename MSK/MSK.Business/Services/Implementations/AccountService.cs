using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using MSK.Business.DTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using System.Text;

namespace MSK.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IUserStore<User> _userStore;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration, IEmailService emailService, IUserStore<User> userStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;
            this._emailService = emailService;
            this._userStore = userStore;
        }
        public async Task Login(LoginModelDto adminLoginViewModel)
        {
            User admin = null;

            admin = await _userManager.FindByEmailAsync(adminLoginViewModel.Email);
            if (admin == null) throw new InvalidUserCredentialException("", "Username or password is wrong!");

            var result = await _signInManager.PasswordSignInAsync(admin, adminLoginViewModel.Password, adminLoginViewModel.RememberMe, false);

            if (!result.Succeeded) throw new InvalidUserCredentialException("", "Username or password is wrong!");
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Register(RegisterModelDto registerModelDto)
        {
            User user = await _userManager.FindByEmailAsync(registerModelDto.Email);
            if (user is not null)
            {
                throw new InvalidUserCredentialException("Email", "The use rwith this email is already exist!");
            }

            user = new User()
            {

                Email = registerModelDto.Email,
                UserName = registerModelDto.UserName,
                FullName = registerModelDto.FullName,



            };


            var result = await _userManager.CreateAsync(user, registerModelDto.Password);


            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));


                if (!string.IsNullOrEmpty(encodedToken))
                {
                    await UserConfirmationEmail(user, encodedToken);
                }
            }
            foreach (var error in result.Errors)
            {
                throw new InvalidUserCredentialException("", "Something went wrong!");
            }
        }

        private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }
        private async Task UserConfirmationEmail(User user, string token)
        {
            string appdomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOption options = new UserEmailOption()
            {
                ToEmails = new List<string>()
                {
                    user.Email
                },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.UserName),
                    new KeyValuePair<string, string>("//UserName//",user.UserName),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appdomain+confirmLink,user.Id,token ,user.Email))
                }
            };

            await _emailService.SendEmailToUserForConfirmation(options);
        }
        
    }
}
