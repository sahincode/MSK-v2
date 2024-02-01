using AutoMapper;
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;

using System.Linq.Expressions;

namespace MSK.Business.Services.Implementations
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;

        public SettingService(ISettingRepository settingRepository, IMapper mapper
            )
        {
            this._settingRepository = settingRepository;
            this._mapper = mapper;
        }
        public async Task Delete(int id)
        {


            var Setting = await this.GetById(id);
            if (Setting is null) throw new NullEntityException("", $"Setting model does not exist in database with {id} id");


            await _settingRepository.CommitAsync();
        }

        public async Task<IEnumerable<SettingGetDto>> GetAllAsync(Expression<Func<Setting, bool>>? expression, params string[]? includes)
        {
            var Settings = await _settingRepository.GetAll(expression, includes);
            List<SettingGetDto> SettingGetDtos = new List<SettingGetDto>();
            foreach (var Setting in Settings)
            {
                SettingGetDto SettingGetDto = _mapper.Map<SettingGetDto>(Setting);
                SettingGetDtos.Add(SettingGetDto);

            }
            return SettingGetDtos;
        }

      

        public async Task<SettingGetDto> GetAsync(Expression<Func<Setting, bool>>? expression, params string[]? includes)
        {
            var Setting = await _settingRepository.GetAll(expression, includes);
            SettingGetDto SettingGetDto = _mapper.Map<SettingGetDto>(Setting);
            return SettingGetDto;
        }

       

        public async Task<Setting> GetById(int id)
        {
            return await _settingRepository.Get(c => c.Id == id);
        }

        public async Task ToggleDelete(int id)
        {
            

            var Setting = await this.GetById(id);
            if (Setting is null) throw new NullEntityException("", $"Setting model does not exist in database with {id} id");
            Setting.IsDeleted = !Setting.IsDeleted;


            await _settingRepository.CommitAsync();
        }

        public async Task Update(SettingUpdateDto SettingUpdateDto)
        {

            var Setting = await this.GetById(SettingUpdateDto.Id);

            if (Setting is null) throw new NullEntityException("", "Setting model does not exist in database with {id} id");


            Setting.Key = SettingUpdateDto.Key;
            Setting.Value = SettingUpdateDto.Value;


            await _settingRepository.CommitAsync();



        }

       
    }
}
