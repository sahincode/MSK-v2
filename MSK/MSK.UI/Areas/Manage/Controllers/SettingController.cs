using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;

namespace MSK.Areas.Manage.Controllers
{
    [Area("Manage")]
            [Authorize(Roles = "Admin,SuperAdmin")]

    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IMapper _mapper;

        public SettingController(ISettingService settingService, IMapper mapper)
        {
            this._settingService = settingService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetAllAsync(null, null);
            if (settings is null)
            {
                return NotFound();
            }

            return View(settings);
        }

        public async Task<IActionResult> Update(int id)
        {
            var setting = await _settingService.GetById(id);
            if (setting is null)
            {
                return NotFound();
            }
            SettingUpdateDto settingUpdateDto = _mapper.Map<SettingUpdateDto>(setting);
            return View(settingUpdateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]

        public async Task<IActionResult> Update(SettingUpdateDto settingUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(settingUpdateDto);
            }
            try
            {
                await _settingService.Update(settingUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "setting");

        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _settingService.Delete(id);
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
                await _settingService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "setting");
        }
    }
}
