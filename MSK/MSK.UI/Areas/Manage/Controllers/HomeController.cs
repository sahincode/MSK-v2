using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.Services.Interfaces;
using System.Data;

namespace Pigga.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class HomeController : Controller
    {
        private readonly IHomeSlideService _homeSlideService;
        private readonly IMapper _mapper;

        public HomeController(IHomeSlideService homeSlideService, IMapper mapper)
        {
            this._homeSlideService = homeSlideService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<HomeSlideLayoutDto> homeSlideLayoutDtos = new List<HomeSlideLayoutDto>();
            var slides = await _homeSlideService.GetAll(s => s.IsDeleted == false);
            foreach (var slide in slides)
            {
                HomeSlideLayoutDto homeSlideLayoutDto = _mapper.Map<HomeSlideLayoutDto>(slide);
                homeSlideLayoutDtos.Add(homeSlideLayoutDto);
  
            }

            return View(homeSlideLayoutDtos);
        }
    }
}
