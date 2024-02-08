using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.InstructionModelDTOs;
using MSK.Business.DTOs.SubDecisionModelDTOs;
using MSK.Business.DTOs.SubInstructionModelDTOs;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;

namespace MSK.UI.Controllers
{
    public class InstructionController : Controller
    {
        private readonly IInstructionService _instructionService;
        private readonly ISubInstructionService _subInstructionService;
        private readonly IMapper _mapper;

        public InstructionController(IInstructionService instructionService 
            ,ISubInstructionService subInstructionService ,IMapper mapper)
        {
            this._instructionService = instructionService;
            this._subInstructionService = subInstructionService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<Instruction> instructions = _instructionService.GetAll(a => !a.IsDeleted).Result.OrderBy(a => a.CreationTime).ToList();
            List<InstructionLayoutDto> instructionLayoutDtos = new List<InstructionLayoutDto>();
            if (instructions is not null)
            {
                foreach (var item in instructions)
                {
                    var instructionLayoutDto = _mapper.Map<InstructionLayoutDto>(item);
                    instructionLayoutDtos.Add(instructionLayoutDto);
                }
            }

            return View(instructionLayoutDtos);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
          List<SubInstruction> subInstructions= _subInstructionService.GetAll(a => !a.IsDeleted&&a.InstructionId==id).Result.OrderBy(a => a.CreationTime).ToList();
            List<SubInstructionLayoutDto> subInstructionLayoutDtos = new List<SubInstructionLayoutDto>();
            if (subInstructions is not null)
            {
                foreach (var item in subInstructions)
                {
                    var subInstructionLayoutDto= _mapper.Map<SubInstructionLayoutDto>(item);
                    subInstructionLayoutDtos.Add(subInstructionLayoutDto);
                }
            }

            return View(subInstructionLayoutDtos);
        }
        public async Task<IActionResult> Instruction(int? id)
        {
            if (id == null || id < 0)
            {
                return NotFound();
            }
            Instruction instruction = await _instructionService.Get(a => !a.IsDeleted && a.Id == id);


            var instructionLayoutDto = _mapper.Map<InstructionLayoutDto>(instruction);
            return View(instructionLayoutDto);
        }
    }
}
