using MSK.Business.DTOs.PressNewDTOs;
using MSK.Business.DTOs.PressNewDTOs;
using MSK.Core.Models;
using System.Linq.Expressions;

namespace MSK.Business.Services.Interfaces
{
    public interface IPressNewService
    {
        Task CreateAsync(PressNewCreateDto entity);
        Task UpdateAsync(PressNewUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<PressNew> GetById(int? id);
        Task<PressNew> Get(Expression<Func<PressNew, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<PressNew>> GetAll(Expression<Func<PressNew, bool>>? predicate, params string[]? include);
    }
}
