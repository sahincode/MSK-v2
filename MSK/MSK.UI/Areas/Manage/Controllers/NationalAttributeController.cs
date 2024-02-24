using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.NationalAttributeModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]

    public class NationalAttributeController : Controller
    {
        private readonly INationalAttributeService _nationalAttributeService;
        private readonly IMapper _mapper;

        public NationalAttributeController(INationalAttributeService nationalAttributeService, IMapper mapper)
        {
            this._nationalAttributeService = nationalAttributeService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int page)
        {
            var histories = await _nationalAttributeService.GetAll(null, null);
            if (histories is null)
            {
                return NotFound();
            }
            List<NationalAttributeIndexDto> historyIndexDtos = new List<NationalAttributeIndexDto>();
            foreach (var slide in histories)
            {
                NationalAttributeIndexDto historyIndexDto = _mapper.Map<NationalAttributeIndexDto>(slide);
                historyIndexDtos.Add(historyIndexDto);
            }
            PaginatedList<NationalAttributeIndexDto> homeSlideIndexDtos = PaginatedList<NationalAttributeIndexDto>.Create
                (historyIndexDtos.AsQueryable(), page, 50);

            return View(homeSlideIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NationalAttributeCreateDto nACreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(nACreateDto);
            }
            try
            {
                await _nationalAttributeService.CreateAsync(nACreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var slide = await _nationalAttributeService.GetById(id);
            if (slide is null)
            {
                return NotFound();
            }
            NationalAttributeUpdateDto historyUpdateDto = _mapper.Map<NationalAttributeUpdateDto>(slide);
            return View(historyUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(NationalAttributeUpdateDto nationalAttributeUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(nationalAttributeUpdate);
            }
            try
            {
                await _nationalAttributeService.UpdateAsync(nationalAttributeUpdate);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "nationalattribute");

        }
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _nationalAttributeService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "nationalattribute");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _nationalAttributeService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "nationalattribute");
        }
    }
}
