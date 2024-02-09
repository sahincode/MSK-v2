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
        private readonly IReferendumRepository _ReferendumRepository;
        private readonly IMapper _mapper;
       

        public ReferendumService(IMapper mapper, IReferendumRepository ReferendumRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._ReferendumRepository = ReferendumRepository;
            
        }
        public async Task CreateAsync(ReferendumCreateDto entity)
        {
            
            Referendum Referendum = _mapper.Map<Referendum>(entity);
           
           
            await _ReferendumRepository.CreateAsync(Referendum);
            await _ReferendumRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            
            Referendum Referendum = await _ReferendumRepository.Get(a => a.Id == id);
            if (Referendum == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
           
            _ReferendumRepository.Delete(Referendum);


        }

        public async Task<Referendum> Get(Expression<Func<Referendum, bool>>? predicate, params string[]? includes)
        {
            return await _ReferendumRepository.Get(predicate, includes) is not null ?
                 await _ReferendumRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Referendum>> GetAll(Expression<Func<Referendum, bool>>? predicate, params string[]? includes)
        {
            return await _ReferendumRepository.GetAll(predicate, includes) is not null ?
                await _ReferendumRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<Referendum> GetById(int? id)
        {
            return await _ReferendumRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var Referendum = await this.GetById(id);
            if (Referendum == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            Referendum.IsDeleted = !Referendum.IsDeleted;
            await _ReferendumRepository.CommitAsync();

        }

        public async Task UpdateAsync(ReferendumUpdateDto entity)
        {

         
            var updatedReferendum = await _ReferendumRepository.Get(a => a.Id == entity.Id);
            if (updatedReferendum == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedReferendum = _mapper.Map(entity, updatedReferendum);


          
            await _ReferendumRepository.CommitAsync();

        }

       
    }
}
