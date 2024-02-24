using AutoMapper;
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
        private readonly IMapper _mapper;

        public AISupportController( IAiService aiService ,IMapper mapper )
        {
            this._aiService = aiService;
            this._mapper = mapper;
        }
        public async Task< IActionResult> Index()
        {
             List<ChatLayoutDto>  chatLayoutDtos = new List<ChatLayoutDto>();
            var chats = await _aiService.GetAll(); 
             if(chats is not null)
            {
                foreach(var chat in chats)
                {
                    ChatLayoutDto chatLayoutDto = _mapper.Map<ChatLayoutDto>(chat);
                    chatLayoutDtos.Add(chatLayoutDto);
                }
            }
            return View(chatLayoutDtos);
        }
        [HttpPost]
        public async  Task SaveUserSection(ChatCreateDto chatCreateDto)
        {
            if (ModelState.IsValid)
            {
               await _aiService.SaveUserSection(chatCreateDto);
            }

        }
        [HttpPost]
        public async Task DeleteUserSection(ChatCreateDto chatCreateDto)
        {
            if (ModelState.IsValid)
            {
                await _aiService.DeleteUserSection(chatCreateDto);
            }

        }
    }
}
