using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.VoterModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;
using System.Data;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class VoterController : Controller
    {
        private readonly IVoterService _VoterService;
        private readonly IMapper _mapper;

        public VoterController(IVoterService VoterService, IMapper mapper)
        {
            this._VoterService = VoterService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int page)
        {
            var slides = await _VoterService.GetAll(null, null);
            if (slides is null)
            {
                return NotFound();
            }
            List<VoterIndexDto> listSlides = new List<VoterIndexDto>();
            foreach (var slide in slides)
            {
                VoterIndexDto VoterIndexDto = _mapper.Map<VoterIndexDto>(slide);
                listSlides.Add(VoterIndexDto);
            }
            PaginatedList<VoterIndexDto> VoterIndexDtos = PaginatedList<VoterIndexDto>.Create
                (listSlides.AsQueryable(), page, 50);

            return View(VoterIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(VoterCreateDto VoterCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(VoterCreateDto);
            }
            try
            {
                await _VoterService.CreateAsync(VoterCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var slide = await _VoterService.GetById(id);
            if (slide is null)
            {
                return NotFound();
            }
            VoterUpdateDto VoterUpdateDto = _mapper.Map<VoterUpdateDto>(slide);
            return View(VoterUpdateDto);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Update(VoterUpdateDto VoterUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(VoterUpdateDto);
            }
            try
            {
                await _VoterService.UpdateAsync(VoterUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Voter");

        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _VoterService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Voter");
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _VoterService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Voter");
        }
    }
}

