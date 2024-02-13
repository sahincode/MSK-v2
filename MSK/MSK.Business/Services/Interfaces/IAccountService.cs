using MSK.Business.DTOs;

namespace MSK.Business.Services.Interfaces
{
    public interface IAccountService
    {
        public Task Register(RegisterModelDto registerModelDto);
        public Task Login(LoginModelDto adminLoginViewModel);
        public Task Logout();


    }
}
