using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.LegislationModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class LegislationController : Controller
    {
        private readonly ILegislationService _legislationService;
        private readonly IMapper _mapper;

        public LegislationController(ILegislationService legislationService, IMapper mapper)
        {
            this._legislationService = legislationService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int page)
        {
            var legislations = await _legislationService.GetAll(null, null);
            if (legislations is null)
            {
                return NotFound();
            }
            List<LegislationIndexDto> legislationIndexDtos = new List<LegislationIndexDto>();
            foreach (var legislation in legislations)
            {
                LegislationIndexDto LegislationIndexDto = _mapper.Map<LegislationIndexDto>(legislation);
                legislationIndexDtos.Add(LegislationIndexDto);
            }
            PaginatedList<LegislationIndexDto> LegislationIndexDtos = PaginatedList<LegislationIndexDto>.Create
                (legislationIndexDtos.AsQueryable(), page, 50);

            return View(LegislationIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LegislationCreateDto legislationCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(legislationCreateDto);
            }
            try
            {
                await _legislationService.CreateAsync(legislationCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var legislation = await _legislationService.GetById(id);
            if (legislation is null)
            {
                return NotFound();
            }
            LegislationUpdateDto legislationUpdateDto = _mapper.Map<LegislationUpdateDto>(legislation);
            return View(legislationUpdateDto);
        }

        public async Task<IActionResult> Update(LegislationUpdateDto legislationUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(legislationUpdateDto);
            }
            try
            {
                await _legislationService.UpdateAsync(legislationUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Legislation");

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _legislationService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Legislation");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _legislationService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Legislation");
        }
    }
}
