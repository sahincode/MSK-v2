using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using System.Data;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeSlideController : Controller
    {
        private readonly IHomeSlideService _homeSlideService;
        private readonly IMapper _mapper;

        public HomeSlideController(IHomeSlideService settingService, IMapper mapper)
        {
            this._homeSlideService = settingService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var settings = await _homeSlideService.GetAll(null, null);
            if (settings is null)
            {
                return NotFound();
            }

            return View(settings);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(HomeSlideCreateDto homeSlideCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(homeSlideCreateDto);
            }
            try
            {
                await _homeSlideService.CreateAsync(homeSlideCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
           

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var setting = await _homeSlideService.GetById(id);
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
        public async Task<IActionResult> Update(HomeSlideUpdateDto homeSlideUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(homeSlideUpdateDto);
            }
            try
            {
                await _homeSlideService.UpdateAsync(homeSlideUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "homeslide");

        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _homeSlideService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "setting");
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _homeSlideService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "setting");
        }
    }
}

