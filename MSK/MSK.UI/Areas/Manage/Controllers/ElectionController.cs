using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSK.Business.DTOs.ElectionModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    public class ElectionController : Controller
    {
        private readonly IElectionService _electionService;
        private readonly IMapper _mapper;
        private readonly IDecisionService _decisionService;
        private readonly IInstructionService _instructionService;
        private readonly ICalendarPlanService _calendarPlanService;
        private readonly IInfoService _infoService;

        public ElectionController(IElectionService ElectionService,
            IMapper mapper, IDecisionService decisionService,
            IInstructionService instructionService, ICalendarPlanService calendarPlanService,
            IInfoService infoService)
        {
            this._electionService = ElectionService;
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
            var calendarPlans = _calendarPlanService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList calendarPlanList = new SelectList(calendarPlans, "Id", "Title");
            ViewData["calendarPlans"] = calendarPlanList;
            var infos = _infoService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList infoList = new SelectList(infos, "Id", "Name");
            ViewData["infos"] = infoList;
            var elections = await _electionService.GetAll(null, "Decision", "Instruction", "Infos", "CalendarPlan");
            if (elections is null)
            {
                return NotFound();
            }
            List<ElectionIndexDto> electionIndexDtos = new List<ElectionIndexDto>();
            foreach (var election in elections)
            {
                ElectionIndexDto ElectionIndexDto = _mapper.Map<ElectionIndexDto>(election);
                electionIndexDtos.Add(ElectionIndexDto);
            }
            PaginatedList<ElectionIndexDto> PElectionIndexDtos = PaginatedList<ElectionIndexDto>.Create
                (electionIndexDtos.AsQueryable(), page, 50);

            return View(PElectionIndexDtos);
        }
        public async Task<IActionResult> Create()
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ElectionCreateDto ElectionCreateDto)
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
                return View(ElectionCreateDto);
            }
            try
            {
                await _electionService.CreateAsync(ElectionCreateDto);
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
            var Election = await _electionService.GetById(id);
            if (Election is null)
            {
                return NotFound();
            }
            ElectionUpdateDto ElectionUpdateDto = _mapper.Map<ElectionUpdateDto>(Election);
            return View(ElectionUpdateDto);
        }
        [HttpPost]

        public async Task<IActionResult> Update(ElectionUpdateDto ElectionUpdateDto)
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
                return View(ElectionUpdateDto);
            }
            try
            {
                await _electionService.UpdateAsync(ElectionUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Election");

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _electionService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Election");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _electionService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Election");
        }
    }
}
