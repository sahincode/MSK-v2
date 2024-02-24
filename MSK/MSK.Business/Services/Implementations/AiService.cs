using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MSK.Business.DTOs.ChatModelDTOs;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Implementations
{
    public  class AiService : IAiService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public AiService( IChatRepository chatRepository ,IMapper mapper,
            IHttpContextAccessor httpContextAccessor ,UserManager<User> userManager)
        {
            this._chatRepository = chatRepository;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
        }

        public async Task< List<Chat>> GetAll()
        {
            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            return    _chatRepository.GetAll(c => c.IsDeleted == false && c.ChatterId == user.Email).Result.ToList();
        }

        public async  Task SaveUserSection(ChatCreateDto entity)
        {
            var user =await  _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            entity.ChatterId = user.Email;
            var chat = _mapper.Map<Chat>(entity);
            await _chatRepository.CreateAsync(chat);
            await _chatRepository.CommitAsync();
          
        }
        public async Task DeleteUserSection(ChatCreateDto entity)
        {
            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            entity.ChatterId = user.Email;
            var chat =  await _chatRepository.Get(c => c.Question.Contains(entity.Question) && c.ChatterId == user.Id);
            if(chat is not null)
            {
                _chatRepository.Delete(chat);
            }
            await _chatRepository.CommitAsync();

        }
    }
}
