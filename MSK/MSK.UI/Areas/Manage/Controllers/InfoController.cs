using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSK.Business.DTOs.InfoModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    public class InfoController : Controller
    {
        private readonly IInfoService _infoService;
        private readonly IMapper _mapper;
        private readonly IDecisionService _decisionService;

        public InfoController(IInfoService infoService,
            IMapper mapper, IDecisionService decisionService)
        {
            this._infoService = infoService;
            this._mapper = mapper;
            this._decisionService = decisionService;
        }
        public async Task<IActionResult> Index(int page)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            var Infos = await _infoService.GetAll(null, null);
            if (Infos is null)
            {
                return NotFound();
            }
            List<InfoIndexDto> InfoIndexDtos = new List<InfoIndexDto>();
            foreach (var Info in Infos)
            {
                InfoIndexDto InfoIndexDto = _mapper.Map<InfoIndexDto>(Info);
                InfoIndexDtos.Add(InfoIndexDto);
            }
            PaginatedList<InfoIndexDto> PInfoIndexDtos = PaginatedList<InfoIndexDto>.Create
                (InfoIndexDtos.AsQueryable(), page, 50);

            return View(PInfoIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(InfoCreateDto InfoCreateDto)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            if (!ModelState.IsValid)
            {
                return View(InfoCreateDto);
            }
            try
            {
                await _infoService.CreateAsync(InfoCreateDto);
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
            var Info = await _infoService.GetById(id);
            if (Info is null)
            {
                return NotFound();
            }
            InfoUpdateDto InfoUpdateDto = _mapper.Map<InfoUpdateDto>(Info);
            return View(InfoUpdateDto);
        }
        [HttpPost]

        public async Task<IActionResult> Update(InfoUpdateDto InfoUpdateDto)
        {
            var decisions = _decisionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList decisionList = new SelectList(decisions, "Id", "Title");
            ViewData["decisions"] = decisionList;
            if (!ModelState.IsValid)
            {
                return View(InfoUpdateDto);
            }
            try
            {
                await _infoService.UpdateAsync(InfoUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Info");

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _infoService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Info");
        }

        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _infoService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "Info");
        }
    }
}
