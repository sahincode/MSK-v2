using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.VoterModelDTOs;

namespace MSK.UI.Controllers
{
    public class IvotingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> VoterLogin(VoterLoginDto voterLoginDto)
        {

            return View();
        }
    }
}
