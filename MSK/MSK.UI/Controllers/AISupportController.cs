using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSK.Business.DTOs.ChatModelDTOs;
using MSK.Business.Services.Interfaces;

namespace MSK.UI.Controllers
{
    [UserAuthorize]
    public class AISupportController : Controller
    {
        private readonly IAiService _aiService;

        public AISupportController( IAiService aiService)
        {
            this._aiService = aiService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async  Task SaveUserSection(ChatCreateDto chatCreateDto)
        {
            if (ModelState.IsValid)
            {
               await _aiService.SaveUserSection(chatCreateDto);
            }

        }
    }
}
