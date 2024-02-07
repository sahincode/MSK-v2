using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSK.Business.DTOs.SubDecisionModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.ViewModels;
using System.Linq;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SubDecisionController : Controller
    {
        private readonly ISubDecisionService _subDecisionService;
        private readonly IMapper _mapper;
        private readonly IDecisionService _decisionService;

        public SubDecisionController(ISubDecisionService subDecisionService,
            IMapper mapper ,IDecisionService decisionService)
        {
            this._subDecisionService = subDecisionService;
            this._mapper = mapper;
            this._decisionService = decisionService;
        }
        public async Task<IActionResult> Index(int page)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            var SubDecisions = await _subDecisionService.GetAll(null, null);
            if (SubDecisions is null)
            {
                return NotFound();
            }
            List<SubDecisionIndexDto> SubDecisionIndexDtos = new List<SubDecisionIndexDto>();
            foreach (var SubDecision in SubDecisions)
            {
                SubDecisionIndexDto SubDecisionIndexDto = _mapper.Map<SubDecisionIndexDto>(SubDecision);
                SubDecisionIndexDtos.Add(SubDecisionIndexDto);
            }
            PaginatedList<SubDecisionIndexDto> PSubDecisionIndexDtos = PaginatedList<SubDecisionIndexDto>.Create
                (SubDecisionIndexDtos.AsQueryable(), page, 50);

            return View(PSubDecisionIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubDecisionCreateDto subDecisionCreateDto)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            if (!ModelState.IsValid)
            {
                return View(subDecisionCreateDto);
            }
            try
            {
                await _subDecisionService.CreateAsync(subDecisionCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            var subDecision = await _subDecisionService.GetById(id);
            if (subDecision is null)
            {
                return NotFound();
            }
            SubDecisionUpdateDto subDecisionUpdateDto = _mapper.Map<SubDecisionUpdateDto>(subDecision);
            return View(subDecisionUpdateDto);
        }

        public async Task<IActionResult> Update(SubDecisionUpdateDto subDecisionUpdateDto)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            if (!ModelState.IsValid)
            {
                return View(subDecisionUpdateDto);
            }
            try
            {
                await _subDecisionService.UpdateAsync(subDecisionUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "SubDecision");

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _subDecisionService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "SubDecision");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _subDecisionService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "SubDecision");
        }
    }
}
