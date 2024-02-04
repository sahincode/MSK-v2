
using MSK.Business.DTOs.HistoryModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IHistoryService
    {
        Task CreateAsync(HistoryCreateDto entity);
        Task UpdateAsync(HistoryUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<History> GetById(int? id);
        Task<History> Get(Expression<Func<History, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<History>> GetAll(Expression<Func<History, bool>>? predicate, params string[]? include);
    }
}
