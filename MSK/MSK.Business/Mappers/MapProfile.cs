using AutoMapper;
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Core.Models;

namespace MSK.Business.Mappers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
           
            CreateMap<SettingUpdateDto, Setting>().ReverseMap();
            CreateMap<SettingGetDto, Setting>().ReverseMap();
           

        }
    }
}
