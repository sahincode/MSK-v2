using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSK.Business.DTOs.InfoModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class InfoController : Controller
    {
        private readonly IInfoService _infoService;
        private readonly IMapper _mapper;
        private readonly IReferendumService _referendumService;
        private readonly IElectionService _electionService;

        public InfoController(IInfoService infoService,
            IMapper mapper, IReferendumService referendumService ,IElectionService electionService)
        {
            this._infoService = infoService;
            this._mapper = mapper;
            this._referendumService = referendumService;
            this._electionService = electionService;
        }
        public async Task<IActionResult> Index(int page)
        {
            var referendums = _referendumService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList referendumList = new SelectList(referendums, "Id", "Name");
            ViewData["referendums"] = referendumList;
            var elections = _electionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList electionList = new SelectList(elections, "Id", "FullName");
            ViewData["elections"] = electionList;
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
            var referendums = _referendumService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList referendumList = new SelectList(referendums, "Id", "Name");
            ViewData["referendums"] = referendumList;
            var elections = _electionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList electionList = new SelectList(elections, "Id", "FullName");
            ViewData["elections"] = electionList;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(InfoCreateDto InfoCreateDto)
        {
            var referendums = _referendumService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList referendumList = new SelectList(referendums, "Id", "Name");
            ViewData["referendums"] = referendumList;
            var elections = _electionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList electionList = new SelectList(elections, "Id", "FullName");
            ViewData["elections"] = electionList;
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
            var referendums = _referendumService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList referendumList = new SelectList(referendums, "Id", "Name");
            ViewData["referendums"] = referendumList;
            var elections = _electionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList electionList = new SelectList(elections, "Id", "FullName");
            ViewData["elections"] = electionList;
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
            var referendums = _referendumService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList referendumList = new SelectList(referendums, "Id", "Name");
            ViewData["referendums"] = referendumList;
            var elections = _electionService.GetAll(d => !d.IsDeleted).Result.ToList();
            SelectList electionList = new SelectList(elections, "Id", "FullName");
            ViewData["elections"] = electionList;
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
