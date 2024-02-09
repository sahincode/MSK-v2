using MSK.Business.DTOs.InfoModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IInfoService
    {
        Task CreateAsync(InfoCreateDto entity);
        Task UpdateAsync(InfoUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<Info> GetById(int? id);
        Task<Info> Get(Expression<Func<Info, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<Info>> GetAll(Expression<Func<Info, bool>>? predicate, params string[]? include);
    }
}
