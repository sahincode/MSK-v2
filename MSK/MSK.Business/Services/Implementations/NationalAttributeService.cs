using AutoMapper;
using MSK.Business.DTOs.NationalAttributeModelDTOs;
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
    public class NationalAttributeService :INationalAttributeService
    {

        private readonly INationalAttributeRepository _nationalAttributeRepository;
        private readonly IMapper _mapper;

        public NationalAttributeService(INationalAttributeRepository nationalAttributeRepository, IMapper mapper
            )
        {
            this._nationalAttributeRepository = nationalAttributeRepository;
            this._mapper = mapper;
        }

        public async Task CreateAsync(NationalAttributeCreateDto entity)
        {

            NationalAttribute homeSlide = _mapper.Map<NationalAttribute>(entity);

            await _nationalAttributeRepository.CreateAsync(homeSlide);
            await _nationalAttributeRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {


            var nationalAttribute = await this.GetById(id);
            if (nationalAttribute is null) throw new NullEntityException("", $"History model does not exist in database with {id} id");


            await _nationalAttributeRepository.CommitAsync();
        }

        public async Task<NationalAttribute> Get(Expression<Func<NationalAttribute, bool>>? predicate, params string[]? includes)
        {
            return await _nationalAttributeRepository.Get(predicate, includes) is not null ?
                await _nationalAttributeRepository.Get(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<NationalAttribute>> GetAll(Expression<Func<NationalAttribute, bool>>? predicate, params string[]? includes)
        {
            return await _nationalAttributeRepository.GetAll(predicate, includes) is not null ?
               await _nationalAttributeRepository.GetAll(predicate, includes) :
               throw new EntityNotFoundException($"The entity with the ID equal to" +
               $" {predicate} was not found in the database.");
        }





        public async Task<NationalAttribute> GetById(int? id)
        {
            return await _nationalAttributeRepository.Get(c => c.Id == id);
        }



        public async Task ToggleDelete(int id)
        {


            var nationalAttribute = await this.GetById(id);
            if (nationalAttribute is null) throw new NullEntityException("", $"History model does not exist in database with {id} id");
            nationalAttribute.IsDeleted = !nationalAttribute.IsDeleted;


            await _nationalAttributeRepository.CommitAsync();
        }

        public async Task UpdateAsync(NationalAttributeUpdateDto nationalAttributeUpdateDto)
        {

            var nationalAttribute = await this.GetById(nationalAttributeUpdateDto.Id);

            if (nationalAttribute is null) throw new NullEntityException("", "History model does not exist in database with {id} id");


            nationalAttribute.InfoStart = nationalAttributeUpdateDto.InfoStart;
            nationalAttribute.InfoMiddle = nationalAttributeUpdateDto.InfoMiddle;
            nationalAttribute.InfoEnd = nationalAttributeUpdateDto.InfoEnd;



            await _nationalAttributeRepository.CommitAsync();



        }
    }
}
