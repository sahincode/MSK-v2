using Microsoft.AspNetCore.Identity;
using MSK.Business.DTOs;
using MSK.Core.Models;

namespace MSK.Business.Services.Interfaces
{
    public interface IAccountService
    {
        public Task Register(RegisterModelDto registerModelDto);
        public Task Login(LoginModelDto adminLoginViewModel);
        public Task Logout();
        public Task GenerateForgetPasswordTokenAsync(User user);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        public  Task UpdateUser(UpdateUserDto updateUserDto);
        public Task ToggleRole(string roleId, string userId);
    }
}
