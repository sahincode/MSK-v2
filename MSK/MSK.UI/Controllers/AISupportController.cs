using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MSK.UI.Controllers
{
    [UserAuthorize]
    public class AISupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
