using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MSK.UI.Controllers
{
    [Authorize(AuthenticationSchemes = "DefaultCookie")]
    public class AISupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
