using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSK.Business.DTOs.CandidateModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Implementations;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;
using System.Data;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CandidateController : Controller
    {
        private readonly ICandidateService _candidateService;
        private readonly IMapper _mapper;
        private readonly IElectionService _electionService;

        public CandidateController(ICandidateService candidateService, 
            IMapper mapper ,IElectionService electionService)
        {
            this._candidateService = candidateService;
            this._mapper = mapper;
            this._electionService = electionService;
        }
        public async Task<IActionResult> Index(int page)
        {
            var elections = _electionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList electionList = new SelectList(elections, "Id", "Name");
            ViewData["elections"] = electionList;
            var candidates = await _candidateService.GetAll(null, null);
            if (candidates is null)
            {
                return NotFound();
            }
            List<CandidateIndexDto> candidateIndexDtos = new List<CandidateIndexDto>();
            foreach (var candidate in candidates)
            {
                CandidateIndexDto candidateIndexDto = _mapper.Map<CandidateIndexDto>(candidate);
                candidateIndexDtos.Add(candidateIndexDto);
            }
            PaginatedList<CandidateIndexDto> PaginatedConttactIndexDtos = PaginatedList<CandidateIndexDto>.Create
                (candidateIndexDtos.AsQueryable(), page, 50);

            return View(PaginatedConttactIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            var elections = _electionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList electionList = new SelectList(elections, "Id", "Name");
            ViewData["elections"] = electionList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidateCreateDto candidateCreateDto)
        {
            var elections = _electionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList electionList = new SelectList(elections, "Id", "Name");
            ViewData["elections"] = electionList;
            if (!ModelState.IsValid)
            {
                return View(candidateCreateDto);
            }
            try
            {
                await _candidateService.CreateAsync(candidateCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var elections = _electionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList electionList = new SelectList(elections, "Id", "Name");
            ViewData["elections"] = electionList;
            var candidate = await _candidateService.GetById(id);
            if (candidate is null)
            {
                return NotFound();
            }
            CandidateUpdateDto candidateUpdateDto = _mapper.Map<CandidateUpdateDto>(candidate);
            return View(candidateUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CandidateUpdateDto candidateUpdateDto)
        {
            var elections = _electionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList electionList = new SelectList(elections, "Id", "Name");
            ViewData["elections"] = electionList;
            if (!ModelState.IsValid)
            {
                return View(candidateUpdateDto);
            }
            try
            {
                await _candidateService.UpdateAsync(candidateUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "candidate");

        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _candidateService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "candidate");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _candidateService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "candidate");
        }
    }
}
