using MSK.Business.DTOs;

namespace MSK.Business.Services.Interfaces
{
    public interface IAccountService
    {
        public Task Login(AdminLoginDto adminLoginViewModel);
        public Task Logout();


    }
}
