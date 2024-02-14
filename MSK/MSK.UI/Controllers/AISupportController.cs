using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MSK.UI.Controllers
{
    [CustomAuthorize]
    public class AISupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
