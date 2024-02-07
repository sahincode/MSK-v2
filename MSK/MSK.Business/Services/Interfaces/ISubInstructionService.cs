using MSK.Business.DTOs.InstructionModelDTOs;
using MSK.Business.DTOs.SubInstructionModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface ISubInstructionService
    {
        Task CreateAsync(SubInstructionCreateDto entity);
        Task UpdateAsync(SubInstructionUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<SubInstruction> GetById(int? id);
        Task<SubInstruction> Get(Expression<Func<SubInstruction, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<SubInstruction>> GetAll(Expression<Func<SubInstruction, bool>>? predicate, params string[]? include);
    }
}
