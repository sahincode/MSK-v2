using MSK.Business.DTOs.ChatModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IAiService
    {
        public Task SaveUserSection(ChatCreateDto entity);
        public Task< List<Chat>> GetAll();
        public  Task DeleteUserSection(ChatCreateDto entity);
    }
}
