using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.PressNewDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.UI.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MSK.UI.Controllers
{
    public class PressNewController : Controller
    {
        private readonly IPressNewService _pressNewService;
        private readonly IMapper _mapper;

        public PressNewController(IPressNewService pressNewService ,IMapper mapper)
        {
            this._pressNewService = pressNewService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int page)
        {
            List<PressNew> pressNews = null;
            try
            {
                 pressNews = _pressNewService.GetAll(pn => pn.IsDeleted == false).Result.ToList();
            }catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            List<PressNewLayoutDto> pressNewLayoutDtos = new List<PressNewLayoutDto>();
            foreach (var neW in pressNews)
            {
                PressNewLayoutDto pressNewLayoutDto = _mapper.Map<PressNewLayoutDto>(neW);
                pressNewLayoutDtos.Add(pressNewLayoutDto);
            }
            PaginatedList<PressNewLayoutDto> paginatedNews = PaginatedList<PressNewLayoutDto>.Create
               (pressNewLayoutDtos.AsQueryable(), page, 1, null);
            return View(paginatedNews);
        }
        public async Task< IActionResult>Detail(int? id)
        {
            PressNew pressNew = null;
            try
            {
                 pressNew = await _pressNewService.GetById(id);
            }catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
           
            PressNewLayoutDto pressNewLayoutDto = _mapper.Map<PressNewLayoutDto>(pressNew);
            return View(pressNewLayoutDto);
        }
    }
}
