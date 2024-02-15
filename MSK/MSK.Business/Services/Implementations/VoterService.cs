using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.Exceptions.OutOfRangesExceptions;
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
using MSK.Business.DTOs.VoterModelDTOs;

namespace MSK.Business.Services.Implementations
{
    public class VoterService :IVoterService
    {
        private readonly IMapper _mapper;
        private readonly IVoterRepository _VoterRepository;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/img/voter";

        public VoterService(IMapper mapper, IVoterRepository VoterRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._VoterRepository = VoterRepository;
            this._env = env;
        }
        public async Task CreateAsync(VoterCreateDto entity)
        {
            string rootPath = _env.WebRootPath;
            Voter Voter = _mapper.Map<Voter>(entity);
            if (entity.Image.ContentType != "image/jpeg" && entity.Image.ContentType != "image/png")
            {
                throw new OutOfRangeImageSizeException("Image", "only png or jpeg file!");
            }
            if (entity.Image.Length > 2097152)
            {
                throw new OutOfRangeImageSizeException("Image", "please upload less than 2 mg");
            }
            Voter.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, entity.Image);
            await _VoterRepository.CreateAsync(Voter);
            await _VoterRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            string rootPath = _env.WebRootPath;
            Voter Voter = await _VoterRepository.Get(a => a.Id == id);
            if (Voter == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, Voter.ImageUrl));
            _VoterRepository.Delete(Voter);


        }

        public async Task<Voter> Get(Expression<Func<Voter, bool>>? predicate, params string[]? includes)
        {
            return await _VoterRepository.Get(predicate, includes) is not null ?
                 await _VoterRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Voter>> GetAll(Expression<Func<Voter, bool>>? predicate, params string[]? includes)
        {
            return await _VoterRepository.GetAll(predicate, includes) is not null ?
                await _VoterRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<Voter> GetById(int? id)
        {
            return await _VoterRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var Voter = await this.GetById(id);
            if (Voter == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            Voter.IsDeleted = !Voter.IsDeleted;
            await _VoterRepository.CommitAsync();

        }

        public async Task UpdateAsync(VoterUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedVoter = await _VoterRepository.Get(a => a.Id == entity.Id);
            if (updatedVoter == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedVoter = _mapper.Map(entity, updatedVoter);


            if (entity.Image is not null)
            {
                if (entity.Image.ContentType != "image/jpeg" && entity.Image.ContentType != "image/png")
                {
                    throw new OutOfRangeImageSizeException("Image", "only png or jpeg file!");
                }
                if (entity.Image.Length > 2097152)
                {
                    throw new OutOfRangeImageSizeException("Image", "please upload less than 2 mg");
                }
                updatedVoter.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, entity.Image);
            }

            await _VoterRepository.CommitAsync();

        }
    }
}
