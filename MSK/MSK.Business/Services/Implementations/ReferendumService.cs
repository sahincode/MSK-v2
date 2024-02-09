using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.ReferendumModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System.Linq.Expressions;

namespace MSK.Business.Services.Implementations
{
    public class ReferendumService :IReferendumService
    {
        private readonly IReferendumRepository _referendumRepository;
        private readonly IDecisionRepository _decisionRepository;
        private readonly IInstructionRepository _instructionRepository;
        private readonly ICalendarPlanRepository _calendarPlanRepository;
        private readonly IMapper _mapper;
       

        public ReferendumService(IMapper mapper, IReferendumRepository ReferendumRepository, 
            IWebHostEnvironment env ,IDecisionRepository decisionRepository ,
            IInstructionRepository instructionRepository ,ICalendarPlanRepository calendarPlanRepository)
        {
            this._mapper = mapper;
            this._referendumRepository = ReferendumRepository;
            this._decisionRepository = decisionRepository;
            this._instructionRepository = instructionRepository;
            this._calendarPlanRepository = calendarPlanRepository;
        }
        public async Task CreateAsync(ReferendumCreateDto entity)
        {
            
            Referendum Referendum = _mapper.Map<Referendum>(entity);
            Decision decision = await  _decisionRepository.Get(d => d.Id == entity.DecisionId);
            if (decision is not null) decision.Referendum = Referendum;
            Instruction instruction = await _instructionRepository.Get(d => d.Id == entity.InstructionId);
            if (instruction is not null) instruction.Referendum = Referendum;
            CalendarPlan calendarPlan = await _calendarPlanRepository.Get(d => d.Id == entity.CalendarPlanId);
            if (calendarPlan is not null) calendarPlan.Referendum = Referendum;

            await _referendumRepository.CreateAsync(Referendum);
            await _referendumRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            
            Referendum referendum = await _referendumRepository.Get(a => a.Id == id);
            if (referendum == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
           
            _referendumRepository.Delete(referendum);
            await _referendumRepository.CommitAsync();


        }

        public async Task<Referendum> Get(Expression<Func<Referendum, bool>>? predicate, params string[]? includes)
        {
            return await _referendumRepository.Get(predicate, includes) is not null ?
                 await _referendumRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Referendum>> GetAll(Expression<Func<Referendum, bool>>? predicate, params string[]? includes)
        {
            return await _referendumRepository.GetAll(predicate, includes) is not null ?
                await _referendumRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<Referendum> GetById(int? id)
        {
            return await _referendumRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var Referendum = await this.GetById(id);
            if (Referendum == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            Referendum.IsDeleted = !Referendum.IsDeleted;
            await _referendumRepository.CommitAsync();

        }

        public async Task UpdateAsync(ReferendumUpdateDto entity)
        {

         
            var updatedReferendum = await _referendumRepository.Get(a => a.Id == entity.Id);
            if (updatedReferendum == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            Decision decision = await _decisionRepository.Get(d => d.Id == entity.DecisionId);
            if (decision is not null) decision.Referendum = updatedReferendum;
            Instruction instruction = await _instructionRepository.Get(d => d.Id == entity.InstructionId);
            if (instruction is not null) instruction.Referendum = updatedReferendum;
            CalendarPlan calendarPlan = await _calendarPlanRepository.Get(d => d.Id == entity.CalendarPlanId);
            if (calendarPlan is not null) calendarPlan.Referendum = updatedReferendum;
            updatedReferendum = _mapper.Map(entity, updatedReferendum);


          
            await _referendumRepository.CommitAsync();

        }

       
    }
}
