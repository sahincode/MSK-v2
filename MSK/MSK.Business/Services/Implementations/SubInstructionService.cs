using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.SubDecisionModelDTOs;
using MSK.Business.Exceptions.SizeExceptions;
using MSK.Business.Exceptions;
using MSK.Business.InternalHelperServices;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MSK.Business.DTOs.SubInstructionModelDTOs;

namespace MSK.Business.Services.Implementations
{
    public class SubInstructionService :ISubInstructionService
    {
        private readonly ISubInstructionRepository _subInstructionRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/pdf/instruction";

        public SubInstructionService(IMapper mapper, ISubInstructionRepository subInstructionRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._subInstructionRepository = subInstructionRepository;
            this._env = env;
        }
        public async Task CreateAsync(SubInstructionCreateDto entity)
        {
            string rootPath = _env.WebRootPath;
            SubInstruction subInstruction = _mapper.Map<SubInstruction>(entity);
            if (entity.Pdf.ContentType != "application/pdf")
            {
                throw new OutOfRangePdfSizeException("Pdf", "only pdf file!");
            }
            if (entity.Pdf.Length > 104857600)
            {
                throw new OutOfRangePdfSizeException("Pdf", "please upload less than 100 mg");
            }
            subInstruction.Url = await FileHelper.SavePdf(rootPath, passPath, entity.Pdf);
            await _subInstructionRepository.CreateAsync(subInstruction);
            await _subInstructionRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            string rootPath = _env.WebRootPath;
            SubInstruction subInstruction = await _subInstructionRepository.Get(a => a.Id == id);
            if (subInstruction == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, subInstruction.Url));
            _subInstructionRepository.Delete(subInstruction);


        }

        public async Task<SubInstruction> Get(Expression<Func<SubInstruction, bool>>? predicate, params string[]? includes)
        {
            return await _subInstructionRepository.Get(predicate, includes) is not null ?
                 await _subInstructionRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<SubInstruction>> GetAll(Expression<Func<SubInstruction, bool>>? predicate, params string[]? includes)
        {
            return await _subInstructionRepository.GetAll(predicate, includes) is not null ?
                await _subInstructionRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<SubInstruction> GetById(int? id)
        {
            return await _subInstructionRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var subInstruction = await this.GetById(id);
            if (subInstruction == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            subInstruction.IsDeleted = !subInstruction.IsDeleted;
            await _subInstructionRepository.CommitAsync();

        }

        public async Task UpdateAsync(SubInstructionUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedSubInstruction = await _subInstructionRepository.Get(a => a.Id == entity.Id);
            if (updatedSubInstruction == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedSubInstruction = _mapper.Map(entity, updatedSubInstruction);


            if (entity.Pdf is not null)
            {
                if (entity.Pdf.ContentType != "application/pdf")

                    throw new OutOfRangePdfSizeException("Pdf", "only pdf file!");

                if (entity.Pdf.Length > 104857600)

                    throw new OutOfRangePdfSizeException("Pdf", "please upload less than 100 mg");

                updatedSubInstruction.Url = await FileHelper.SavePdf(rootPath, passPath, entity.Pdf);
            }

            await _subInstructionRepository.CommitAsync();

        }

      
    }
}
