using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.CalendarPlanModelDTOs;
using MSK.Business.DTOs.DecisionModelDTOs;
using MSK.Business.DTOs.InfoModelDTOs;
using MSK.Business.DTOs.InstructionModelDTOs;
using MSK.Business.DTOs.ReferendumModelDTOs;
using MSK.Business.DTOs.SubInstructionModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;

namespace MSK.UI.Controllers
{
    public class ReferendumController : Controller
    {
        private readonly IReferendumService _referendumService;
        private readonly IMapper _mapper;
        private readonly IInfoService _infoService;
        private readonly ICalendarPlanService _calendarPlanService;
        private readonly IDecisionService _decisionService;
        private readonly ISubInstructionService _subInstructionService;

        public ReferendumController( IReferendumService referendumService ,IMapper mapper,
            IInfoService infoService ,
            ICalendarPlanService calendarPlanService,IDecisionService decisionService ,ISubInstructionService subInstructionService)
        {
            this._referendumService = referendumService;
            this._mapper = mapper;
            this._infoService = infoService;
            this._calendarPlanService = calendarPlanService;
            this._decisionService = decisionService;
            this._subInstructionService = subInstructionService;
        }
        public async Task< IActionResult>Index(int id=3)
        {
            Referendum referendum = null;
            try
            {
                 referendum = await _referendumService.Get(r => r.Id == id && !r.IsDeleted, "Decision", "Instruction", "Infos", "CalendarPlan");
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            ReferendumLayoutDto referendumLayoutDto = _mapper.Map<ReferendumLayoutDto>(referendum);
            

            return View(referendumLayoutDto);
        }
        public async Task<IActionResult> Info(int id)
        {
            Info info = null;
            InfoLayoutDto infoLayoutDto = null;
            try
            {
                info =  await _infoService.GetById(id);
                 infoLayoutDto = _mapper.Map<InfoLayoutDto>(info); 
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return View(infoLayoutDto);
        }
        public async Task<IActionResult> Plan(int id)
        {
            CalendarPlan calendarPlan = null;
            CalendarPlanLayoutDto calendarPlanLayoutDto = null;
            try
            {
                calendarPlan = await _calendarPlanService.GetById(id);
                calendarPlanLayoutDto = _mapper.Map<CalendarPlanLayoutDto>(calendarPlan);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return View(calendarPlanLayoutDto);
        }
        public async Task<IActionResult> Decision(int id)
        {
            Decision decision = null;
            DecisionLayoutDto decisionLayoutDto = null;
            try
            {
                decision = await _decisionService.Get(d=>d.Id==id, "SubDecisions");
                decisionLayoutDto = _mapper.Map<DecisionLayoutDto>(decision);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return View(decisionLayoutDto);
        }
        public async Task<IActionResult> Instruction(int id)
        {
           List<SubInstruction> subInstructions = null;
            List<SubInstructionLayoutDto> subInstructionLayoutDtos=  new List<SubInstructionLayoutDto>();
            try
            {
                subInstructions =  _subInstructionService.GetAll(d => d.InstructionId == id).Result.ToList();
                foreach(var subIns in subInstructions)
                {
                  SubInstructionLayoutDto  instructionLayoutDto = _mapper.Map<SubInstructionLayoutDto>(subIns);
                    subInstructionLayoutDtos.Add(instructionLayoutDto);
                }
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return View(subInstructionLayoutDtos);
        }
    }
}
