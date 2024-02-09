using MSK.Business.DTOs.ReferendumModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IReferendumService
    {
        Task CreateAsync(ReferendumCreateDto entity);
        Task UpdateAsync(ReferendumUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<Referendum> GetById(int? id);
        Task<Referendum> Get(Expression<Func<Referendum, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<Referendum>> GetAll(Expression<Func<Referendum, bool>>? predicate, params string[]? include);
    }
}
