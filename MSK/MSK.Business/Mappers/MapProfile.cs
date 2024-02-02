using AutoMapper;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Core.Models;

namespace MSK.Business.Mappers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
           
            //setting model mapping profile
            CreateMap<SettingUpdateDto, Setting>().ReverseMap();
            CreateMap<SettingGetDto, Setting>().ReverseMap();
            //Home slide model mapping profile
            CreateMap<HomeSlideCreateDto, HomeSlide>().ReverseMap();
            CreateMap<HomeSlideUpdateDto, HomeSlide>().ReverseMap();
            CreateMap<HomeSlideIndexDto, HomeSlide>().ReverseMap();
            CreateMap<HomeSlideLayoutDto, HomeSlide>().ReverseMap();


        }
    }
}
