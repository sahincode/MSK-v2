using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.InstructionModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class InstructionController : Controller
    {
        private readonly IInstructionService  _instructionService;
        private readonly IMapper _mapper;

        public InstructionController(IInstructionService instructionService, IMapper mapper)
        {
            this._instructionService = instructionService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int page)
        {
            var instructions = await _instructionService.GetAll(null, null);
            if (instructions is null)
            {
                return NotFound();
            }
            List<InstructionIndexDto> instructionIndexDtos = new List<InstructionIndexDto>();
            foreach (var instruction in instructions)
            {
                InstructionIndexDto instructionIndexDto = _mapper.Map<InstructionIndexDto>(instruction);
                instructionIndexDtos.Add(instructionIndexDto);
            }
            PaginatedList<InstructionIndexDto> pInstructionIndexDtos = PaginatedList<InstructionIndexDto>.Create
                (instructionIndexDtos.AsQueryable(), page, 50);

            return View(pInstructionIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(InstructionCreateDto instructionCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(instructionCreateDto);
            }
            try
            {
                await _instructionService.CreateAsync(instructionCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var instruction = await _instructionService.GetById(id);
            if (instruction is null)
            {
                return NotFound();
            }
            InstructionUpdateDto instructionUpdateDto = _mapper.Map<InstructionUpdateDto>(instruction);
            return View(instructionUpdateDto);
        }

        public async Task<IActionResult> Update(InstructionUpdateDto instructionUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(instructionUpdateDto);
            }
            try
            {
                await _instructionService.UpdateAsync(instructionUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Instruction");

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _instructionService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Instruction");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _instructionService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Instruction");
        }
    }
}
