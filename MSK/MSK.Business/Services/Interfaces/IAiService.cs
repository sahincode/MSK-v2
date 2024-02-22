using MSK.Business.DTOs.ChatModelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IAiService
    {
        public Task SaveUserSection(ChatCreateDto entity);
    }
}
