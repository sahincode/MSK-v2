using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.ReferendumModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;

namespace MSK.UI.Controllers
{
    public class ReferendumController : Controller
    {
        private readonly IReferendumService _referendumService;
        private readonly IMapper _mapper;

        public ReferendumController( IReferendumService referendumService ,IMapper mapper )
        {
            this._referendumService = referendumService;
            this._mapper = mapper;
        }
        public async Task< IActionResult>Index(int id=3)
        {
            Referendum referendum = null;
            try
            {
                 referendum = await _referendumService.Get(r => r.Id == id && !r.IsDeleted, "Decision", "Instruction", "Infos", "CalendarPlan");
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            ReferendumLayoutDto referendumLayoutDto = _mapper.Map<ReferendumLayoutDto>(referendum);
            

            return View(referendumLayoutDto);
        }
    }
}
