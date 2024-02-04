using MSK.Business.DTOs.NationalAttributeModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface INationalAttributeService
    {
        Task CreateAsync(NationalAttributeCreateDto entity);
        Task UpdateAsync(NationalAttributeUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<NationalAttribute> GetById(int? id);
        Task<NationalAttribute> Get(Expression<Func<NationalAttribute, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<NationalAttribute>> GetAll(Expression<Func<NationalAttribute, bool>>? predicate, params string[]? include);
    }
}
