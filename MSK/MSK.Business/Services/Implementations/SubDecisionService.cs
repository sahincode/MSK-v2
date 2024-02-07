using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.Exceptions.SizeExceptions;
using MSK.Business.Exceptions;
using MSK.Business.InternalHelperServices;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MSK.Business.DTOs.SubDecisionModelDTOs;
using MSK.Business.Services.Interfaces;

namespace MSK.Business.Services.Implementations
{
    public class SubDecisionService:ISubDecisionService
    {
        private readonly ISubDecisionRepository _subDecisionRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/pdf/decision";

        public SubDecisionService(IMapper mapper, ISubDecisionRepository subDecisionRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._subDecisionRepository = subDecisionRepository;
            this._env = env;
        }
        public async Task CreateAsync(SubDecisionCreateDto entity)
        {
            string rootPath = _env.WebRootPath;
            SubDecision subDecision = _mapper.Map<SubDecision>(entity);
            if (entity.Pdf.ContentType != "application/pdf")
            {
                throw new OutOfRangePdfSizeException("Pdf", "only pdf file!");
            }
            if (entity.Pdf.Length > 104857600)
            {
                throw new OutOfRangePdfSizeException("Pdf", "please upload less than 100 mg");
            }
            subDecision.Url = await FileHelper.SavePdf(rootPath, passPath, entity.Pdf);
            await _subDecisionRepository.CreateAsync(subDecision);
            await _subDecisionRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            string rootPath = _env.WebRootPath;
            SubDecision subDecision = await _subDecisionRepository.Get(a => a.Id == id);
            if (subDecision == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, subDecision.Url));
            _subDecisionRepository.Delete(subDecision);


        }

        public async Task<SubDecision> Get(Expression<Func<SubDecision, bool>>? predicate, params string[]? includes)
        {
            return await _subDecisionRepository.Get(predicate, includes) is not null ?
                 await _subDecisionRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<SubDecision>> GetAll(Expression<Func<SubDecision, bool>>? predicate, params string[]? includes)
        {
            return await _subDecisionRepository.GetAll(predicate, includes) is not null ?
                await _subDecisionRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<SubDecision> GetById(int? id)
        {
            return await _subDecisionRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var subDecision = await this.GetById(id);
            if (subDecision == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            subDecision.IsDeleted = !subDecision.IsDeleted;
            await _subDecisionRepository.CommitAsync();

        }

        public async Task UpdateAsync(SubDecisionUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedsubDecision = await _subDecisionRepository.Get(a => a.Id == entity.Id);
            if (updatedsubDecision == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedsubDecision = _mapper.Map(entity, updatedsubDecision);


            if (entity.Pdf is not null)
            {
                if (entity.Pdf.ContentType != "application/pdf")

                    throw new OutOfRangePdfSizeException("Pdf", "only pdf file!");

                if (entity.Pdf.Length > 104857600)

                    throw new OutOfRangePdfSizeException("Pdf", "please upload less than 100 mg");

                updatedsubDecision.Url = await FileHelper.SavePdf(rootPath, passPath, entity.Pdf);
            }

            await _subDecisionRepository.CommitAsync();

        }
    }
}
