using MSK.Business.DTOs.ElectionModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IElectionService
    {
        Task CreateAsync(ElectionCreateDto entity);
        Task UpdateAsync(ElectionUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<Election> GetById(int? id);
        Task<Election> Get(Expression<Func<Election, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<Election>> GetAll(Expression<Func<Election, bool>>? predicate, params string[]? include);
    }
}
