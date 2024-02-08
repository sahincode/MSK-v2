using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.DecisionModelDTOs;
using MSK.Business.DTOs.SubDecisionModelDTOs;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;

namespace MSK.UI.Controllers
{
    public class DecisionController : Controller
    {
        private readonly IDecisionService _decisionService;
        private readonly IMapper _mapper;
        private readonly ISubDecisionService _subDecisionService;

        public DecisionController(IDecisionService decisionService ,
            IMapper mapper ,ISubDecisionService subDecisionService)
        {
            this._decisionService = decisionService;
            this._mapper = mapper;
            this._subDecisionService = subDecisionService;
        }
        public async Task<IActionResult> Index()
        {
            List<Decision> decisions = _decisionService.GetAll(a => !a.IsDeleted).Result.OrderBy(a => a.CreationTime).ToList();
            List<DecisionLayoutDto> decisionLayoutDtos = new List<DecisionLayoutDto>();
            if (decisions is not null)
            {
                foreach (var item in decisions)
                {
                    var decisionLayoutDto = _mapper.Map<DecisionLayoutDto>(item);
                    decisionLayoutDtos.Add(decisionLayoutDto);
                }
            }

            return View(decisionLayoutDtos);
        }
        public async Task<IActionResult> Detail( int? id)
        {
            if( id ==null || id < 0)
            {
                return NotFound();
            }
            var subDecisions =   _subDecisionService.GetAll(sd => sd.DecisionId == id).Result.ToList();
           List<SubDecisionLayoutDto> subDecisionLayoutDtos= new List<SubDecisionLayoutDto>();
            foreach( var item in subDecisions)
            {
                SubDecisionLayoutDto subDecisionLayoutDto = _mapper.Map<SubDecisionLayoutDto>(item);
                subDecisionLayoutDtos.Add(subDecisionLayoutDto);
            }
            return View(subDecisionLayoutDtos);
        }
    }
}
