using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.ContactModelDTOs;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.DTOs.PressNewDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.UI.ViewModels;

using System.Diagnostics;

namespace MSK.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeSlideService _homeSlideService;
        private readonly IMapper _mapper;
        private readonly IPressNewService _pressNewService;
        private readonly IContactService _contactService;

        public HomeController(IHomeSlideService homeSlideService ,
            IMapper mapper ,IPressNewService pressNewService ,
            IContactService contactService)
        {
            this._homeSlideService = homeSlideService;
            this._mapper = mapper;
            this._pressNewService = pressNewService;
            this._contactService = contactService;
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
        public async Task<IActionResult> Contact()
        {
            var contacts =  _contactService.GetAll(c => c.IsDeleted == false).Result.ToList();
            List<ContactLayoutDto> contactLayoutDtos = new List<ContactLayoutDto>();
            if(contacts is not null){
                foreach(var contact in contacts)
                {
                    var contactLayoutDto = _mapper.Map<ContactLayoutDto>(contact);
                    contactLayoutDtos.Add(contactLayoutDto);
                }
            }
            return View(contactLayoutDtos);
        }
        public async Task<IActionResult> Search(string ?query  ,int page)
        { if( query is null)
            {
                ModelState.AddModelError("", "Query can not be null or less than 3 charcters!");
                return View("Search");

            }
            List<PressNew> pressNews=  _pressNewService.GetAll(pn=>pn.Title.Trim().ToLower().Contains(query.Trim().ToLower()) || pn.Description.Trim().ToLower().Contains(query.Trim().ToLower()) ).Result.ToList();
            if(pressNews is null)
            {
                return NotFound();
            }
           List<PressNewLayoutDto> pressNewLayoutDtos= new List<PressNewLayoutDto>();
             foreach(var neW in pressNews)
            {
                PressNewLayoutDto pressNewLayoutDto = _mapper.Map<PressNewLayoutDto>(neW);
                pressNewLayoutDtos.Add(pressNewLayoutDto);
            }
            PaginatedList<PressNewLayoutDto> paginatedNews = PaginatedList<PressNewLayoutDto>.Create
               (pressNewLayoutDtos.AsQueryable(), page, 1 ,query);
            return View(paginatedNews);
        }

        
    }
}