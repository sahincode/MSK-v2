using MSK.Business.DTOs.AccredationModelDTOs;
using MSK.Business.DTOs.AccredationModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IAccredationService
    {
        Task CreateAsync(AccredationCreateDto entity);
        Task UpdateAsync(AccredationUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<Accredation> GetById(int? id);
        Task<Accredation> Get(Expression<Func<Accredation, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<Accredation>> GetAll(Expression<Func<Accredation, bool>>? predicate, params string[]? include);
    }
}
