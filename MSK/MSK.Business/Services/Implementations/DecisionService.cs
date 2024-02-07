using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.DecisionModelDTOs;

using MSK.Business.Exceptions;
using MSK.Business.Exceptions.SizeExceptions;
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

namespace MSK.Business.Services.Implementations
{
    public class DecisionService :IDecisionService
    {
        private readonly IDecisionRepository _decisionRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/pdf/accredation";

        public DecisionService(IDecisionRepository DecisionRepository,
            IMapper mapper,IWebHostEnvironment env
            )
        {
            this._decisionRepository = DecisionRepository;
            this._mapper = mapper;
            this._env = env;
        }

        public async Task CreateAsync(DecisionCreateDto entity)
        {
            

            Decision decision = _mapper.Map<Decision>(entity);
           
            await _decisionRepository.CreateAsync(decision);
            await _decisionRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
           

            var decision = await this.GetById(id);
            if (decision is null) throw new NullEntityException("", $"Decision model does not exist in database with {id} id");
            
            _decisionRepository.Delete(decision);

            await _decisionRepository.CommitAsync();
        }

        public async Task<Decision> Get(Expression<Func<Decision, bool>>? predicate, params string[]? includes)
        {
            return await _decisionRepository.Get(predicate, includes) is not null ?
                await _decisionRepository.Get(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Decision>> GetAll(Expression<Func<Decision, bool>>? predicate, params string[]? includes)
        {
            return await _decisionRepository.GetAll(predicate, includes) is not null ?
               await _decisionRepository.GetAll(predicate, includes) :
               throw new EntityNotFoundException($"The entity with the ID equal to" +
               $" {predicate} was not found in the database.");
        }





        public async Task<Decision> GetById(int? id)
        {
            return await _decisionRepository.Get(c => c.Id == id);
        }



        public async Task ToggleDelete(int id)
        {


            var decision = await this.GetById(id);
            if (decision is null) throw new NullEntityException("", $"Decision model does not exist in database with {id} id");
            decision.IsDeleted = !decision.IsDeleted;


            await _decisionRepository.CommitAsync();
        }

        public async Task UpdateAsync(DecisionUpdateDto decisionUpdateDto)
        {
            string rootPath = _env.WebRootPath;
            var decision = await this.GetById(decisionUpdateDto.Id);

            if (decision is null) throw new NullEntityException("", "Decision model does not exist in database with {id} id");
         

            decisionUpdateDto.Title = decision.Title;
            

            await _decisionRepository.CommitAsync();
        }
    }
}
