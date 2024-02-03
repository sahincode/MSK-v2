using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.DTOs.PressNewDTOs;
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.ViewModels;
using System.Data;

namespace MSK.UI.Areas.Manage.Controllers
{
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
            var slides = await _pressNewService.GetAll(null, null);
            if (slides is null)
            {
                return NotFound();
            }
            List<HomeSlideIndexDto> listSlides = new List<HomeSlideIndexDto>();
            foreach (var slide in slides)
            {
                HomeSlideIndexDto homeSlideIndexDto = _mapper.Map<HomeSlideIndexDto>(slide);
                listSlides.Add(homeSlideIndexDto);
            }
            PaginatedList<HomeSlideIndexDto> homeSlideIndexDtos = PaginatedList<HomeSlideIndexDto>.Create
                (listSlides.AsQueryable(), page, 50);

            return View(homeSlideIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
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
            var setting = await _pressNewService.GetById(id);
            if (setting is null)
            {
                return NotFound();
            }
            SettingUpdateDto settingUpdateDto = _mapper.Map<SettingUpdateDto>(setting);
            return View(settingUpdateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
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
