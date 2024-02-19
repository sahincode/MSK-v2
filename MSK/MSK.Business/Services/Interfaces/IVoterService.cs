using MSK.Business.DTOs.VoterModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IVoterService
    {
        Task CreateAsync(VoterCreateDto entity);
        Task UpdateAsync(VoterUpdateDto entity);
        Task Delete(string id);
        Task VoteAsync(int candidateId);
        Task<Voter> GetById(string id);
        Task<Voter> Get(Expression<Func<Voter
            , bool>>? predicate, params string[]? includes);
        Task<IEnumerable<Voter>> GetAll(Expression<Func<Voter
            , bool>>? predicate, params string[]? includes);
    }
}
