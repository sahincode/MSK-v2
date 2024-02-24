using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.DTOs.PressNewDTOs;
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;
using System.Data;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class PressNewController : Controller
    {
        private readonly IPressNewService _pressNewService;
        private readonly IMapper _mapper;

        public PressNewController(IPressNewService pressNewService, IMapper mapper)
        {
            this._pressNewService = pressNewService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int page)
        {
            var news = await _pressNewService.GetAll(null, null);
            if (news is null)
            {
                return NotFound();
            }
            List<PressNewIndexDto> listSlides = new List<PressNewIndexDto>();
            foreach (var neW in news)
            {
                PressNewIndexDto pressNewIndexDto = _mapper.Map<PressNewIndexDto>(neW);
                listSlides.Add(pressNewIndexDto);
            }
            PaginatedList<PressNewIndexDto> pressNewIndexDtos = PaginatedList<PressNewIndexDto>.Create
                (listSlides.AsQueryable(), page, 50);

            return View(pressNewIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PressNewCreateDto pressNewCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(pressNewCreateDto);
            }
            try
            {
                await _pressNewService.CreateAsync(pressNewCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var neW = await _pressNewService.GetById(id);
            if (neW is null)
            {
                return NotFound();
            }
            PressNewUpdateDto pressNewUpdateDto = _mapper.Map<PressNewUpdateDto>(neW);
            return View(pressNewUpdateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
       
        public async Task<IActionResult> Update(PressNewUpdateDto pressNewUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(pressNewUpdateDto);
            }
            try
            {
                await _pressNewService.UpdateAsync(pressNewUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "pressNew");

        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _pressNewService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "pressNew");
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _pressNewService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "pressNew");
        }
    }
}
