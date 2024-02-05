using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.AccredationModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.ViewModels;

namespace MSK.UI.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccredationController : Controller
    {
        private readonly IAccredationService _accredationService;
        private readonly IMapper _mapper;

        public AccredationController(IAccredationService accredationService, IMapper mapper)
        {
            this._accredationService = accredationService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(int page)
        {
            var news = await _accredationService.GetAll(null, null);
            if (news is null)
            {
                return NotFound();
            }
            List<AccredationIndexDto> listSlides = new List<AccredationIndexDto>();
            foreach (var neW in news)
            {
                AccredationIndexDto accredationIndexDto = _mapper.Map<AccredationIndexDto>(neW);
                listSlides.Add(accredationIndexDto);
            }
            PaginatedList<AccredationIndexDto> accredationIndexDtos = PaginatedList<AccredationIndexDto>.Create
                (listSlides.AsQueryable(), page, 50);

            return View(accredationIndexDtos);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AccredationCreateDto accredationCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(accredationCreateDto);
            }
            try
            {
                await _accredationService.CreateAsync(accredationCreateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var neW = await _accredationService.GetById(id);
            if (neW is null)
            {
                return NotFound();
            }
            AccredationUpdateDto accredationUpdateDto = _mapper.Map<AccredationUpdateDto>(neW);
            return View(accredationUpdateDto);
        }

        public async Task<IActionResult> Update(AccredationUpdateDto accredationUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(accredationUpdateDto);
            }
            try
            {
                await _accredationService.UpdateAsync(accredationUpdateDto);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "accredation");

        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _accredationService.Delete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "accredation");
        }
      
        public async Task<IActionResult> ToggleDelete(int id)
        {
            try
            {
                await _accredationService.ToggleDelete(id);
            }
            catch (NullEntityException ex)
            {
                return NotFound();

            }
            return RedirectToAction("index", "accredation");
        }
    }
}
