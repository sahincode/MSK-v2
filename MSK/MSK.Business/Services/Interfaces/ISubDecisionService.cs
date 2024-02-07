using MSK.Business.DTOs.SubDecisionModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface ISubDecisionService
    {
        Task CreateAsync(SubDecisionCreateDto entity);
        Task UpdateAsync(SubDecisionUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<SubDecision> GetById(int? id);
        Task<SubDecision> Get(Expression<Func<SubDecision, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<SubDecision>> GetAll(Expression<Func<SubDecision, bool>>? predicate, params string[]? include);
    }
}
