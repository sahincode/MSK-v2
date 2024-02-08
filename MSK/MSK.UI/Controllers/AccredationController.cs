using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.AccredationModelDTOs;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;

namespace MSK.UI.Controllers
{

    public class AccredationController : Controller
    {
        private readonly IAccredationService _accredationService;
        private readonly IMapper _mapper;

        public AccredationController(IAccredationService accredationService, IMapper mapper)
        {
            this._accredationService = accredationService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            Accredation accredation = _accredationService.GetAll(a => !a.IsDeleted).Result.OrderBy(a => a.CreationTime).FirstOrDefault();
            AccredationLayoutDto accredationLayoutDto = new AccredationLayoutDto();
            if (accredation is not null) accredationLayoutDto = _mapper.Map<AccredationLayoutDto>(accredation);
            return View(accredationLayoutDto);
        }
    }
}
