using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSK.Business.DTOs.SubInstructionModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]


    public class SubInstructionController : Controller
    {

        private readonly ISubInstructionService _subInstructionService;
        private readonly IMapper _mapper;
        private readonly IInstructionService _instructionService;

        public SubInstructionController(ISubInstructionService subInstructionService,
            IMapper mapper, IInstructionService instructionService)
        {
            this._subInstructionService = subInstructionService;
            this._mapper = mapper;
            this._instructionService = instructionService;
        }
        public async Task<IActionResult> Index(int page)
        {
            var instructions =  _instructionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList instructionList = new SelectList(instructions, "Id", "Name");
            ViewData["instructionList"] = instructionList;
            var subinstructions = await _subInstructionService.GetAll(null, null);
            if (subinstructions is null)
            {
                return NotFound();
            }
            List<SubInstructionIndexDto> subInstructionIndexDtos = new List<SubInstructionIndexDto>();
            foreach (var subinstruction in subinstructions)
            {
                SubInstructionIndexDto subInstructionIndexDto = _mapper.Map<SubInstructionIndexDto>(subinstruction);
                subInstructionIndexDtos.Add(subInstructionIndexDto);
            }
            PaginatedList<SubInstructionIndexDto> PsubinstructionIndexDtos = PaginatedList<SubInstructionIndexDto>.Create
                (subInstructionIndexDtos.AsQueryable(), page, 50);

            return View(PsubinstructionIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            var instructions = _instructionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList instructionList = new SelectList(instructions, "Id", "Name");
            ViewData["instructions"] = instructionList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubInstructionCreateDto subInstructionCreateDto)
        {
            var instructions = _instructionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList instructionList = new SelectList(instructions, "Id", "Name");
            ViewData["instructions"] = instructionList;
            if (!ModelState.IsValid)
            {
                return View(subInstructionCreateDto);
            }
            try
            {
                await _subInstructionService.CreateAsync(subInstructionCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var instructions = _instructionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList instructionList = new SelectList(instructions, "Id", "Title");
            ViewData["instructions"] = instructionList;
            var subInstruction = await _subInstructionService.GetById(id);
            if (subInstruction is null)
            {
                return NotFound();
            }
            SubInstructionUpdateDto subInstructionUpdateDto = _mapper.Map<SubInstructionUpdateDto>(subInstruction);
            return View(subInstructionUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SubInstructionUpdateDto
            subInstructionUpdateDto)
        {
            var instructions = _instructionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList instructionList = new SelectList(instructions, "Id", "Title");
            ViewData["instructions"] = instructionList;
            if (!ModelState.IsValid)
            {
                return View(subInstructionUpdateDto);
            }
            try
            {
                await _subInstructionService.UpdateAsync(subInstructionUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "subinstruction");

        }
        [Authorize(Roles = "SuperAdmin")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _subInstructionService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "subinstruction");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _subInstructionService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "subinstruction");
        }
    }
}
