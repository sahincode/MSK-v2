using MSK.Business.DTOs.CandidateModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface ICandidateService
    {
        Task CreateAsync(CandidateCreateDto entity);
        Task UpdateAsync(CandidateUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<Candidate> GetById(int? id);
        Task<Candidate> Get(Expression<Func<Candidate, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<Candidate>> GetAll(Expression<Func<Candidate, bool>>? predicate, params string[]? include);
    }
}
