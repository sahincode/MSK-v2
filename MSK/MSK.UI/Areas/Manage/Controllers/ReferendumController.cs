using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSK.Business.DTOs.ReferendumModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ReferendumController : Controller
    {
        private readonly IReferendumService _referendumService;
        private readonly IMapper _mapper;
        private readonly IDecisionService _decisionService;
        private readonly IInstructionService _instructionService;
        private readonly ICalendarPlanService _calendarPlanService;
        private readonly IInfoService _infoService;

        public ReferendumController(IReferendumService referendumService,
            IMapper mapper, IDecisionService decisionService ,
            IInstructionService instructionService, ICalendarPlanService calendarPlanService,
            IInfoService infoService)
        {
            this._referendumService = referendumService;
            this._mapper = mapper;
            this._decisionService = decisionService;
            this._instructionService = instructionService;
            this._calendarPlanService = calendarPlanService;
            this._infoService = infoService;
        }
        public async Task<IActionResult> Index(int page)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            var instructions = _instructionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList instructionList = new SelectList(instructions, "Id", "Name");
            ViewData["instructions"] = instructionList;
            var calendarPlans =_calendarPlanService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList calendarPlanList = new SelectList(calendarPlans, "Id", "Title");
            ViewData["calendarPlans"] = calendarPlanList;
            var infos = _infoService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList infoList = new SelectList(infos, "Id", "Name");
            ViewData["infos"] = infoList;
            var referendums = await _referendumService.GetAll(null, "Decision","Instruction" ,"Infos" ,"CalendarPlan");
            if (referendums is null)
            {
                return NotFound();
            }
            List<ReferendumIndexDto> referendumIndexDtos = new List<ReferendumIndexDto>();
            foreach (var referendum in referendums)
            {
                ReferendumIndexDto referendumIndexDto = _mapper.Map<ReferendumIndexDto>(referendum);
                referendumIndexDtos.Add(referendumIndexDto);
            }
            PaginatedList<ReferendumIndexDto> PreferendumIndexDtos = PaginatedList<ReferendumIndexDto>.Create
                (referendumIndexDtos.AsQueryable(), page, 50);

            return View(PreferendumIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            var instructions = _instructionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList instructionList = new SelectList(instructions, "Id", "Name");
            ViewData["instructions"] = instructionList;
            var calendarPlans =_calendarPlanService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList calendarPlanList = new SelectList(calendarPlans, "Id", "Title");
            ViewData["calendarPlans"] = calendarPlanList;
            var infos = _infoService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList infoList = new SelectList(infos, "Id", "Name");
            ViewData["infos"] = infoList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReferendumCreateDto referendumCreateDto)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            var instructions = _instructionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList instructionList = new SelectList(instructions, "Id", "Name");
            ViewData["instructions"] = instructionList;
            var calendarPlans = _calendarPlanService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList calendarPlanList = new SelectList(calendarPlans, "Id", "Title");
            ViewData["calendarPlans"] = calendarPlanList;
            var infos = _infoService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList infoList = new SelectList(infos, "Id", "Name");
            ViewData["infos"] = infoList;
            
            if (!ModelState.IsValid)
            {
                return View(referendumCreateDto);
            }
            try
            {
                await _referendumService.CreateAsync(referendumCreateDto);
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
            var instructions = _instructionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList instructionList = new SelectList(instructions, "Id", "Name");
            ViewData["instructions"] = instructionList;
            var calendarPlans = _calendarPlanService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList calendarPlanList = new SelectList(calendarPlans, "Id", "Title");
            ViewData["calendarPlans"] = calendarPlanList;
            var infos = _infoService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList infoList = new SelectList(infos, "Id", "Name");
            ViewData["infos"] = infoList;
            var referendum = await _referendumService.GetById(id);
            if (referendum is null)
            {
                return NotFound();
            }
            ReferendumUpdateDto referendumUpdateDto = _mapper.Map<ReferendumUpdateDto>(referendum);
            return View(referendumUpdateDto);
        }
        [HttpPost]

        public async Task<IActionResult> Update(ReferendumUpdateDto referendumUpdateDto)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            var instructions = _instructionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList instructionList = new SelectList(instructions, "Id", "Name");
            ViewData["instructions"] = instructionList;
            var calendarPlans = _calendarPlanService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList calendarPlanList = new SelectList(calendarPlans, "Id", "Title");
            ViewData["calendarPlans"] = calendarPlanList;
            var infos = _infoService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList infoList = new SelectList(infos, "Id", "Name");
            ViewData["infos"] = infoList;
           
            if (!ModelState.IsValid)
            {
                return View(referendumUpdateDto);
            }
            try
            {
                await _referendumService.UpdateAsync(referendumUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "referendum");

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _referendumService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "referendum");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _referendumService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "referendum");
        }
    }
}
