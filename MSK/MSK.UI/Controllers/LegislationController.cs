using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.LegislationModelDTOs;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;

namespace MSK.UI.Controllers
{
    public class LegislationController : Controller
    {
        private readonly ILegislationService _legislationService;
        private readonly IMapper _mapper;

        public LegislationController(ILegislationService legislationService, IMapper mapper)
        {
            this._legislationService = legislationService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
           List<Legislation> legislations = _legislationService.GetAll(a => !a.IsDeleted).Result.OrderBy(a => a.CreationTime).ToList();
            List<LegislationLayoutDto> legislationLayoutDtos = new List<LegislationLayoutDto>();
            if (legislations is not null)
            {
                foreach (var item in legislations)
                {
                    var legislationLayoutDto= _mapper.Map<LegislationLayoutDto>(item);
                    legislationLayoutDtos.Add(legislationLayoutDto);
                }
            } 
               
            return View(legislationLayoutDtos);
        }
    }
}
