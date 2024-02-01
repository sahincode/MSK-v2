
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Core.Models;
using System.Linq.Expressions;

namespace MSK.Business.Services.Interfaces
{
    public interface ISettingService
    {

        public Task Update(SettingUpdateDto settingUpdateDto);
        public Task Delete(int id);
        public Task ToggleDelete(int id);
        public Task<IEnumerable<SettingGetDto>> GetAllAsync(Expression<Func<Setting, bool>>? expression, params string[]? includes);
        public Task<SettingGetDto> GetAsync(Expression<Func<Setting, bool>>? expression, params string[]? includes);
        public Task<Setting> GetById(int id);
    }
}
