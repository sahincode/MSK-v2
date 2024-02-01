using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;

namespace MSK.Business.Services.Implementations
{
    public class LayoutService
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public LayoutService(ISettingService settingService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            this._settingService = settingService;
            this._httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
        }
        public async Task<IEnumerable<SettingGetDto>> GetSettings()
        {
            var settingService = await _settingService.GetAllAsync(s => s.IsDeleted == false);
            return settingService;
        }
        public async Task<User> GetUser()
        {
            User user = null;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            }
            return user;
        }
    }
}
