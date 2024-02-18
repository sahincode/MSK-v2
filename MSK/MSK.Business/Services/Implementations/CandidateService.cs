using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.CandidateModelDTOs;
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
using MSK.Business.Exceptions.OutOfRangesExceptions;

namespace MSK.Business.Services.Implementations
{
    public class CandidateService :ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/img/candidate";

        public CandidateService(IMapper mapper, ICandidateRepository CandidateRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._candidateRepository = CandidateRepository;
            this._env = env;
        }
        public async Task CreateAsync(CandidateCreateDto entity)
        {
            string rootPath = _env.WebRootPath;
            Candidate candidate = _mapper.Map<Candidate>(entity);
            if (entity.Image.ContentType != "image/png" && entity.Image.ContentType != "image/jpeg")
            {
                throw new OutOfRangeImageSizeException("Image", "only Image file!");
            }
            if (entity.Image.Length > 104857600)
            {
                throw new OutOfRangeImageSizeException("Image", "please upload less than 100 mg");
            }
            candidate.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, entity.Image);
            await _candidateRepository.CreateAsync(candidate);
            await _candidateRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            string rootPath = _env.WebRootPath;
            Candidate candidate = await _candidateRepository.Get(a => a.Id == id);
            if (candidate == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, candidate.ImageUrl));
            _candidateRepository.Delete(candidate);


        }

        public async Task<Candidate> Get(Expression<Func<Candidate, bool>>? predicate, params string[]? includes)
        {
            return await _candidateRepository.Get(predicate, includes) is not null ?
                 await _candidateRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<Candidate>> GetAll(Expression<Func<Candidate, bool>>? predicate, params string[]? includes)
        {
            return await _candidateRepository.GetAll(predicate, includes) is not null ?
                await _candidateRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<Candidate> GetById(int? id)
        {
            var candidate = await _candidateRepository.Get(a => a.Id == id);
            if (candidate is null) throw new EntityNotFoundException("", $"The entity with the ID equal to" +
                $" {id} was not found in the database.");
            return candidate;
        }



        public async Task ToggleDelete(int id)
        {
            var candidate = await this.GetById(id);
            if (candidate == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            candidate.IsDeleted = !candidate.IsDeleted;
            await _candidateRepository.CommitAsync();

        }

        public async Task UpdateAsync(CandidateUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedCandidate = await _candidateRepository.Get(a => a.Id == entity.Id);
            if (updatedCandidate == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedCandidate = _mapper.Map(entity, updatedCandidate);


            if (entity.Image is not null)
            {
                if (entity.Image.ContentType != "image/png" && entity.Image.ContentType != "image/jpeg")
                {
                    throw new OutOfRangeImageSizeException("Image", "only Image file!");
                }
                if (entity.Image.Length > 104857600)
                {
                    throw new OutOfRangeImageSizeException("Image", "please upload less than 100 mg");
                }
                updatedCandidate.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, entity.Image);
            }

            await _candidateRepository.CommitAsync();

        }
    }
}
