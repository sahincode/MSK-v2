using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MSK.Business.DTOs.ElectionModelDTOs;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.Services.Implementations;
using MSK.Business.Services.Interfaces;
using MSK.UI.ViewModels;
using System.Data;

namespace Pigga.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class HomeController : Controller
    {
        private readonly IElectionService _electionService;
        private readonly IMapper _mapper;
        private readonly LayoutService _layoutService;

        public HomeController(IElectionService electionService,
            IMapper mapper ,LayoutService layoutService)
        {
            this._electionService = electionService;
            this._mapper = mapper;
            this._layoutService = layoutService;
        }
        public async Task<IActionResult> Index()
        {
            var elections =  await _electionService.GetAll(null, "Candidates");
            List<ElectionIndexDto> electionIndexDtos = new List<ElectionIndexDto>();
            foreach(var election in elections)
            {
                ElectionIndexDto electionIndexDto = _mapper.Map<ElectionIndexDto>(election);
                electionIndexDtos.Add(electionIndexDto);
            }
            AdminHomeViewModel adminHomeViewModel = new AdminHomeViewModel
            {
                ElectionIndexDtos = electionIndexDtos,
            };
            return View(adminHomeViewModel);
        }
        public async Task<IActionResult> GetElectionVotesAsJson()
        {
            var result = await _layoutService.CalculateElectionsVotes();
            return Json( new { data = result["ElectionVotes"], labels = result["NameOfElections"] });
        }
        public async Task<IActionResult> GeCandidatesVotes(int id)
        {
            var result = await _layoutService.CalculateCandiateVotes(id);
            return Json(new { data = result["CandidateVotes"], labels = result["NameOfCandidates"] });
        }
    }
}
