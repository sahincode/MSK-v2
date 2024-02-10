using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.DecisionModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DecisionController : Controller
    {
        private readonly IDecisionService _decisionService;
        private readonly IMapper _mapper;

        public DecisionController(IDecisionService decisionService, IMapper mapper)
        {
            this._decisionService = decisionService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int page)
        {
            var Decisions = await _decisionService.GetAll(null, null);
            if (Decisions is null)
            {
                return NotFound();
            }
            List<DecisionIndexDto> DecisionIndexDtos = new List<DecisionIndexDto>();
            foreach (var Decision in Decisions)
            {
                DecisionIndexDto DecisionIndexDto = _mapper.Map<DecisionIndexDto>(Decision);
                DecisionIndexDtos.Add(DecisionIndexDto);
            }
            PaginatedList<DecisionIndexDto> PaginatedDecisionIndexDtos = PaginatedList<DecisionIndexDto>.Create
                (DecisionIndexDtos.AsQueryable(), page, 50);

            return View(PaginatedDecisionIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DecisionCreateDto DecisionCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(DecisionCreateDto);
            }
            try
            {
                await _decisionService.CreateAsync(DecisionCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var Decision = await _decisionService.GetById(id);
            if (Decision is null)
            {
                return NotFound();
            }
            DecisionUpdateDto DecisionUpdateDto = _mapper.Map<DecisionUpdateDto>(Decision);
            return View(DecisionUpdateDto);
        }

        public async Task<IActionResult> Update(DecisionUpdateDto DecisionUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(DecisionUpdateDto);
            }
            try
            {
                await _decisionService.UpdateAsync(DecisionUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Decision");

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _decisionService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Decision");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _decisionService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Decision");
        }
    }
}
