using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.DTOs.PressNewDTOs;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;
using System.Diagnostics;

namespace MSK.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeSlideService _homeSlideService;
        private readonly IMapper _mapper;
        private readonly IPressNewService _pressNewService;

        public HomeController(IHomeSlideService homeSlideService ,IMapper mapper ,IPressNewService pressNewService)
        {
            this._homeSlideService = homeSlideService;
            this._mapper = mapper;
            this._pressNewService = pressNewService;
        }

        public async Task< IActionResult> Index()
        {
            List<HomeSlideLayoutDto> homeSlideLayoutDtos = new List<HomeSlideLayoutDto>();
            var slides = await _homeSlideService.GetAll(s => s.IsDeleted == false);
            foreach (var slide in slides)
            {
                HomeSlideLayoutDto homeSlideLayoutDto = _mapper.Map<HomeSlideLayoutDto>(slide);
                homeSlideLayoutDtos.Add(homeSlideLayoutDto);

            }
            List<PressNewLayoutDto> pressNewLayoutDtos = new List<PressNewLayoutDto>();
            var news = _pressNewService.GetAll(s => s.IsDeleted == false).Result.OrderBy(n => n.CreationTime);
            foreach (var neW in news)
            {
                PressNewLayoutDto pressNewLayoutDto = _mapper.Map<PressNewLayoutDto>(neW);
                pressNewLayoutDtos.Add(pressNewLayoutDto);

            }
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                HomeSlideLayoutDtos = homeSlideLayoutDtos,
                PressNewLayoutDtos = pressNewLayoutDtos,

            };

            return View(homeIndexViewModel);
        }

        
    }
}