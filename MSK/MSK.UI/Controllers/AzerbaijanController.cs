using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.HistoryModelDTOs;
using MSK.Business.DTOs.NationalAttributeModelDTOs;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;

namespace MSK.UI.Controllers
{
    public class AzerbaijanController : Controller
    {
        private readonly IHistoryService _historyService;
        private readonly INationalAttributeService _nationalAttributeService;
        private readonly IMapper _mapper;

        public AzerbaijanController(IHistoryService historyService ,
            INationalAttributeService nationalAttributeService, IMapper mapper)
        {
            this._historyService = historyService;
            this._nationalAttributeService = nationalAttributeService;
            this._mapper = mapper;
        }
        public async Task< IActionResult>HaC()
        {
            var history = await _historyService.Get(h => !h.IsDeleted);
           HistoryLayoutDto historyLayoutDto= _mapper.Map<HistoryLayoutDto>(history);
            return View(historyLayoutDto);
        }
        public async Task< IActionResult > NH()
        {
            var nationalAttribite = await _nationalAttributeService.Get(na => !na.IsDeleted);

            NationalAttributeLayoutDto nationalAttributeLayout = _mapper.Map<NationalAttributeLayoutDto>(nationalAttribite);
            return View(nationalAttributeLayout);
        }
    }
}
