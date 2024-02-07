using MSK.Business.DTOs.DecisionModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IDecisionService
    {
        Task CreateAsync(DecisionCreateDto entity);
        Task UpdateAsync(DecisionUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<Decision> GetById(int? id);
        Task<Decision> Get(Expression<Func<Decision, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<Decision>> GetAll(Expression<Func<Decision, bool>>? predicate, params string[]? include);
    }
}
