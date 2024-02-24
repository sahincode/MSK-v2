using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.HistoryModelDTOs;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;
using System.Data;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [ValidateAntiForgeryToken]
    public class HistoryController : Controller
    {
        private readonly IHistoryService _historyService;
        private readonly IMapper _mapper;

        public HistoryController(IHistoryService historyService, IMapper mapper)
        {
            this._historyService = historyService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int page)
        {
            var histories = await _historyService.GetAll(null, null);
            if (histories is null)
            {
                return NotFound();
            }
            List<HistoryIndexDto> historyIndexDtos = new List<HistoryIndexDto>();
            foreach (var slide in histories)
            {
                HistoryIndexDto historyIndexDto = _mapper.Map<HistoryIndexDto>(slide);
                historyIndexDtos.Add(historyIndexDto);
            }
            PaginatedList<HistoryIndexDto> histroyIndexDtos = PaginatedList<HistoryIndexDto>.Create
                (historyIndexDtos.AsQueryable(), page, 50);

            return View(histroyIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HistoryCreateDto historyCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(historyCreateDto);
            }
            try
            {
                await _historyService.CreateAsync(historyCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var slide = await _historyService.GetById(id);
            if (slide is null)
            {
                return NotFound();
            }
            HistoryUpdateDto historyUpdateDto = _mapper.Map<HistoryUpdateDto>(slide);
            return View(historyUpdateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(HistoryUpdateDto historyUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(historyUpdateDto);
            }
            try
            {
                await _historyService.UpdateAsync(historyUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "history");

        }
        [Authorize(Roles = "SuperAdmin")]
       
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _historyService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "history");
        }
     
        
        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _historyService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "history");
        }
    }
}
