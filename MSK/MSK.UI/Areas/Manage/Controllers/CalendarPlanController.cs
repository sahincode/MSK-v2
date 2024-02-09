using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSK.Business.DTOs.CalendarPlanModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    public class CalendarPlanController : Controller
    {
        private readonly ICalendarPlanService _calendarPlanService;
        private readonly IMapper _mapper;
        private readonly IDecisionService _decisionService;

        public CalendarPlanController(ICalendarPlanService calendarPlanService,
            IMapper mapper, IDecisionService decisionService)
        {
            this._calendarPlanService = calendarPlanService;
            this._mapper = mapper;
            this._decisionService = decisionService;
        }
        public async Task<IActionResult> Index(int page)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            var calendarPlans = await _calendarPlanService.GetAll(null, null);
            if (calendarPlans is null)
            {
                return NotFound();
            }
            List<CalendarPlanIndexDto> calendarPlanIndexDtos = new List<CalendarPlanIndexDto>();
            foreach (var calendarPlan in calendarPlans)
            {
                CalendarPlanIndexDto calendarPlanIndexDto = _mapper.Map<CalendarPlanIndexDto>(calendarPlan);
                calendarPlanIndexDtos.Add(calendarPlanIndexDto);
            }
            PaginatedList<CalendarPlanIndexDto> PcalendarPlanIndexDtos = PaginatedList<CalendarPlanIndexDto>.Create
                (calendarPlanIndexDtos.AsQueryable(), page, 50);

            return View(PcalendarPlanIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CalendarPlanCreateDto calendarPlanCreateDto)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            if (!ModelState.IsValid)
            {
                return View(calendarPlanCreateDto);
            }
            try
            {
                await _calendarPlanService.CreateAsync(calendarPlanCreateDto);
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
            var calendarPlan = await _calendarPlanService.GetById(id);
            if (calendarPlan is null)
            {
                return NotFound();
            }
            CalendarPlanUpdateDto calendarPlanUpdateDto = _mapper.Map<CalendarPlanUpdateDto>(calendarPlan);
            return View(calendarPlanUpdateDto);
        }
        [HttpPost]

        public async Task<IActionResult> Update(CalendarPlanUpdateDto calendarPlanUpdateDto)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            if (!ModelState.IsValid)
            {
                return View(calendarPlanUpdateDto);
            }
            try
            {
                await _calendarPlanService.UpdateAsync(calendarPlanUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "calendarPlan");

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _calendarPlanService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "calendarPlan");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _calendarPlanService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "calendarPlan");
        }
    }
}
