using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.DecisionModelDTOs;
using MSK.Business.DTOs.InstructionModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Implementations
{
    public class InstructonService :IInstructionService
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/pdf/accredation";

        public InstructonService(IInstructionRepository instructionRepository,
            IMapper mapper, IWebHostEnvironment env
            )
        {
            this._instructionRepository = instructionRepository;
            this._mapper = mapper;
            this._env = env;
        }

        public async Task CreateAsync(InstructionCreateDto entity)
        {


            Instruction instruction = _mapper.Map<Instruction>(entity);

            await _instructionRepository.CreateAsync(instruction);
            await _instructionRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {


            var decision = await this.GetById(id);
            if (decision is null) throw new NullEntityException("", $"Decision model does not exist in database with {id} id");

            _instructionRepository.Delete(decision);

            await _instructionRepository.CommitAsync();
        }

        public async Task<Instruction> Get(Expression<Func<Instruction, bool>>? predicate, params string[]? includes)
        {
            return await _instructionRepository.Get(predicate, includes) is not null ?
                await _instructionRepository.Get(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Instruction>> GetAll(Expression<Func<Instruction, bool>>? predicate, params string[]? includes)
        {
            return await _instructionRepository.GetAll(predicate, includes) is not null ?
               await _instructionRepository.GetAll(predicate, includes) :
               throw new EntityNotFoundException($"The entity with the ID equal to" +
               $" {predicate} was not found in the database.");
        }





        public async Task<Instruction> GetById(int? id)
        {
            return await _instructionRepository.Get(c => c.Id == id);
        }



        public async Task ToggleDelete(int id)
        {


            var instruction = await this.GetById(id);
            if (instruction is null) throw new NullEntityException("", $"Decision model does not exist in database with {id} id");
            instruction.IsDeleted = !instruction.IsDeleted;


            await _instructionRepository.CommitAsync();
        }

        public async Task UpdateAsync(InstructionUpdateDto instructionUpdateDto)
        {
            string rootPath = _env.WebRootPath;
            var instruction = await this.GetById(instructionUpdateDto.Id);

            if (instruction is null) throw new NullEntityException("", "Instruction model does not exist in database with {id} id");


            instruction.Name = instructionUpdateDto.Name;


            await _instructionRepository.CommitAsync();
        }
    }
}
