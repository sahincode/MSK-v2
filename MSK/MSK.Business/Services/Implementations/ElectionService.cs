using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.ElectionModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System.Linq.Expressions;

namespace MSK.Business.Services.Implementations
{
    public class ElectionService : IElectionService
    {
        private readonly IElectionRepository _electionRepository;
        private readonly IDecisionRepository _decisionRepository;
        private readonly IInstructionRepository _instructionRepository;
        private readonly ICalendarPlanRepository _calendarPlanRepository;
        private readonly IMapper _mapper;


        public ElectionService(IMapper mapper, IElectionRepository electionRepository,
            IWebHostEnvironment env, IDecisionRepository decisionRepository,
            IInstructionRepository instructionRepository, ICalendarPlanRepository calendarPlanRepository)
        {
            this._mapper = mapper;
            this._electionRepository = electionRepository;
            this._decisionRepository = decisionRepository;
            this._instructionRepository = instructionRepository;
            this._calendarPlanRepository = calendarPlanRepository;
        }
        public async Task CreateAsync(ElectionCreateDto entity)
        {

            Election election = _mapper.Map<Election>(entity);
            Decision decision = await _decisionRepository.Get(d => d.Id == entity.DecisionId);
            if (decision is not null) decision.Election = election;
            Instruction instruction = await _instructionRepository.Get(d => d.Id == entity.InstructionId);
            if (instruction is not null) instruction.Election = election;
            CalendarPlan calendarPlan = await _calendarPlanRepository.Get(d => d.Id == entity.CalendarPlanId);
            if (calendarPlan is not null) calendarPlan.Election = election;

            await _electionRepository.CreateAsync(election);
            await _electionRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {

            Election Election = await _electionRepository.Get(a => a.Id == id);
            if (Election == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");

            _electionRepository.Delete(Election);
            await _electionRepository.CommitAsync();


        }

        public async Task<Election> Get(Expression<Func<Election, bool>>? predicate, params string[]? includes)
        {
            return await _electionRepository.Get(predicate, includes) is not null ?
                 await _electionRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Election>> GetAll(Expression<Func<Election, bool>>? predicate, params string[]? includes)
        {
            return await _electionRepository.GetAll(predicate, includes) is not null ?
                await _electionRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<Election> GetById(int? id)
        {
            return await _electionRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var Election = await this.GetById(id);
            if (Election == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            Election.IsDeleted = !Election.IsDeleted;
            await _electionRepository.CommitAsync();

        }

        public async Task UpdateAsync(ElectionUpdateDto entity)
        {


            var updatedElection = await _electionRepository.Get(a => a.Id == entity.Id);
            if (updatedElection == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            Decision decision = await _decisionRepository.Get(d => d.Id == entity.DecisionId);
            if (decision is not null) decision.Election = updatedElection;
            Instruction instruction = await _instructionRepository.Get(d => d.Id == entity.InstructionId);
            if (instruction is not null) instruction.Election = updatedElection;
            CalendarPlan calendarPlan = await _calendarPlanRepository.Get(d => d.Id == entity.CalendarPlanId);
            if (calendarPlan is not null) calendarPlan.Election = updatedElection;
            updatedElection = _mapper.Map(entity, updatedElection);



            await _electionRepository.CommitAsync();

        }

    }
}
