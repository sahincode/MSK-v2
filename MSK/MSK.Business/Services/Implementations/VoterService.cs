using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using MSK.Business.DTOs.VoterModelDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Exceptions.OutOfRangesExceptions;
using MSK.Business.InternalHelperServices;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System.Linq.Expressions;

namespace MSK.Business.Services.Implementations
{
    public class VoterService :IVoterService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Voter> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IVoterRepository _voterRepository;
        public const string passPath = "assets/img/voter";

        public VoterService(IMapper mapper,
            UserManager<Voter> userManager, IWebHostEnvironment env ,IVoterRepository voterRepository)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._env = env;
            this._voterRepository = voterRepository;

        }
        public async Task CreateAsync(VoterCreateDto entity)
        {
            Voter voter = await _userManager.FindByNameAsync(entity.FinCode);
            if (voter is not null)
            {
                throw new InvalidUserCredentialException("Email", "The use rwith this email is already exist!");
            }

            string rootPath = _env.WebRootPath;
            voter = _mapper.Map<Voter>(entity);
            if (entity.Image.ContentType != "image/jpeg" && entity.Image.ContentType != "image/png")
            {
                throw new OutOfRangeImageSizeException("Image", "only png or jpeg file!");
            }
            if (entity.Image.Length > 2097152)
            {
                throw new OutOfRangeImageSizeException("Image", "please upload less than 2 mg");
            }
            voter.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, entity.Image);
            voter.UserName = entity.FinCode;
         
            var result = await _userManager.CreateAsync(voter, entity.FinCode);

            if (!result.Succeeded)
            {
                throw new InvalidUserCredentialException("", "Something went wrong!");
            }
            await _userManager.AddToRoleAsync(voter, "Voter");



        }

        public async Task Delete(string  id)
        {
            string rootPath = _env.WebRootPath;
            Voter Voter = await _voterRepository.Get(a => a.Id == id);
            if (Voter == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, Voter.ImageUrl));
            _voterRepository.Delete(Voter);
             await _voterRepository.CommitAsync();

        }

        public async Task<Voter> Get(Expression<Func<Voter, bool>>? predicate, params string[]? includes)
        {
            return await _voterRepository.Get(predicate, includes) is not null ?
                await _voterRepository.Get(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{predicate} was not found in the database.");
        }


        public async Task<IEnumerable<Voter>> GetAll(Expression<Func<Voter
            , bool>>? predicate, params string[]? includes)
        {
            return await _voterRepository.GetAll(predicate, includes) is not null ?
               await _voterRepository.GetAll(predicate, includes) :
               throw new EntityNotFoundException($"The entity with the ID equal to" +
               $" {predicate} was not found in the database.");
        }

        public async Task<Voter> GetById(string id)
        {
            return await _voterRepository.Get(a => a.Id == id);
        }



       

        public async Task UpdateAsync(VoterUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedVoter = await _voterRepository.Get(a => a.Id == entity.Id);
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

            await _voterRepository.CommitAsync();

            }

        
    }
    }
