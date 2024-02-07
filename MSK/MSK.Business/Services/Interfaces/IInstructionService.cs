using MSK.Business.DTOs.InstructionModelDTOs;
using MSK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Interfaces
{
    public interface IInstructionService
    {
        Task CreateAsync(InstructionCreateDto entity);
        Task UpdateAsync(InstructionUpdateDto entity);
        Task Delete(int id);
        Task ToggleDelete(int id);
        Task<Instruction> GetById(int? id);
        Task<Instruction> Get(Expression<Func<Instruction, bool>>? predicate, params string[]? includes);
        Task<IEnumerable<Instruction>> GetAll(Expression<Func<Instruction, bool>>? predicate, params string[]? include);
    }
}
