using MSK.Business.DTOs.LegislationModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface ILegislationService
    {
        Task CreateAsync(LegislationCreateDto entity);
        Task UpdateAsync(LegislationUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<Legislation> GetById(int? id);
        Task<Legislation> Get(Expression<Func<Legislation, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<Legislation>> GetAll(Expression<Func<Legislation, bool>>? predicate, params string[]? include);
    }
}
