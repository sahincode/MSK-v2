using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MSK.Business.DTOs;
using MSK.Business.Exceptions;
using MSK.Business.InternalHelperServices;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration, IEmailService emailService
            , IUserStore<User> userStore,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;
            this._emailService = emailService;
            this._userStore = userStore;
            this._httpContextAccessor = httpContextAccessor;
            this._env = env;
            this._roleManager = roleManager;
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
                throw new InvalidUserCredentialException("Email", "The use with this email is already exist!");
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
                await _userManager.AddToRoleAsync(user ,"User");
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

        public async Task GenerateForgetPasswordTokenAsync(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));


            if (!string.IsNullOrEmpty(encodedToken))
            {
                await SendResetPasswordEmail(user, encodedToken);
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
        private async Task SendResetPasswordEmail(User user, string token)
        {
            string appdomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmLink = _configuration.GetSection("Application:ForgetPassword").Value;

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

            await _emailService.SendEmailForForgetPassword(options);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(resetPasswordDto.UserId), resetPasswordDto.Token, resetPasswordDto.NewPassword);
        }

        public async Task UpdateUser(UpdateUserDto updateUserDto)
        {
            string passPath = "admin/img/user";
            string rootPath = _env.WebRootPath;
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new NullEntityException($"Unable to load user with ID '{_userManager.GetUserId(_httpContextAccessor.HttpContext.User)}'.");
            }
            if (updateUserDto.NewPassword is not null && updateUserDto.Password is not null)
            {


                var changePasswordResult = await _userManager.ChangePasswordAsync(user, updateUserDto.Password, updateUserDto.NewPassword);

                if (changePasswordResult.Succeeded)
                {
                    user.UserName = updateUserDto.UserName;
                    await _signInManager.RefreshSignInAsync(user);


                }

                foreach (var error in changePasswordResult.Errors)
                {
                    throw new InvalidUserCredentialException(string.Empty, error.Description);
                }
            }
            if (updateUserDto.Image is not null)
            {
                if (user.ImageUrl is not null)
                    File.Delete(Path.Combine(rootPath, passPath, user.ImageUrl));

                user.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, updateUserDto.Image);
                user.UserName = updateUserDto.UserName;
                await _userManager.UpdateAsync(user);
                await _signInManager.RefreshSignInAsync(user);


            }

            user.UserName = updateUserDto.UserName;
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);



        }
        public async Task ToggleRole(string roleId, string userId)
        {
            // Find the user and role by their IDs
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(roleId);

            if (user == null || role == null)
            {
                throw new NullEntityException("", "User with this id or role with this id does not exist.");
            }

            // Check if the user has the role
            var userHasRole = await _userManager.IsInRoleAsync(user, role.Name);

            if (userHasRole)
            {
                // User has the role, so remove it
                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

                if (!result.Succeeded)
                {
                    throw new NotChangedRoleException("", "Something went wrong in role changing!");
                }
            }
            else
            {
                // User doesn't have the role, so add it
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    throw new NotChangedRoleException("", "Something went wrong in role changing!");
                }

            }



        }
    }
}
