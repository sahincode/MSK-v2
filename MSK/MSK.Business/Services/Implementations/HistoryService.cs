using AutoMapper;
using MSK.Business.DTOs.HistoryModelDTOs;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Exceptions.OutOfRangesExceptions;
using MSK.Business.InternalHelperServices;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using MSK.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSK.Business.Services.Implementations
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistorRepository _historyRepository;
        private readonly IMapper _mapper;

        public HistoryService(IHistorRepository historyRepository, IMapper mapper
            )
        {
            this._historyRepository = historyRepository;
            this._mapper = mapper;
        }

        public async Task CreateAsync(HistoryCreateDto entity)
        {

            History homeSlide = _mapper.Map<History>(entity);

            await _historyRepository.CreateAsync(homeSlide);
            await _historyRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {


            var History = await this.GetById(id);
            if (History is null) throw new NullEntityException("", $"History model does not exist in database with {id} id");


            await _historyRepository.CommitAsync();
        }

        public async Task<History> Get(Expression<Func<History, bool>>? predicate, params string[]? includes)
        {
            return await _historyRepository.Get(predicate, includes) is not null ?
                await _historyRepository.Get(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<History>> GetAll(Expression<Func<History, bool>>? predicate, params string[]? includes)
        {
            return await _historyRepository.GetAll(predicate, includes) is not null ?
               await _historyRepository.GetAll(predicate, includes) :
               throw new EntityNotFoundException($"The entity with the ID equal to" +
               $" {predicate} was not found in the database.");
        }





        public async Task<History> GetById(int? id)
        {
            return await _historyRepository.Get(c => c.Id == id);
        }



        public async Task ToggleDelete(int id)
        {


            var history = await this.GetById(id);
            if (history is null) throw new NullEntityException("", $"History model does not exist in database with {id} id");
            history.IsDeleted = !history.IsDeleted;


            await _historyRepository.CommitAsync();
        }

        public async Task UpdateAsync(HistoryUpdateDto HistoryUpdateDto)
        {

            var History = await this.GetById(HistoryUpdateDto.Id);

            if (History is null) throw new NullEntityException("", "History model does not exist in database with {id} id");


            History.InfoStart = HistoryUpdateDto.InfoStart;
            History.InfoMiddle = HistoryUpdateDto.InfoMiddle;
            History.InfoEnd = HistoryUpdateDto.InfoEnd;



            await _historyRepository.CommitAsync();



        }


    }
}
