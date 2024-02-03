using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MSK.Business.DTOs.PressNewDTOs;
using MSK.Business.Exceptions;
using MSK.Business.Exceptions.OutOfRangesExceptions;
using MSK.Business.InternalHelperServices;
using MSK.Business.Services.Interfaces;
using MSK.Core.Models;
using MSK.Core.Repositories;
using System.Linq.Expressions;

namespace MSK.Business.Services.Implementations
{
    public class PressNewService :IPressNewService
    {
        private readonly IMapper _mapper;
        private readonly IPressNewRepository _pressNewRepository;
        private readonly IWebHostEnvironment _env;
        public const string passPath = "assets/img/pressnew";

        public PressNewService(IMapper mapper, IPressNewRepository pressNewRepository, IWebHostEnvironment env)
        {
            this._mapper = mapper;
            this._pressNewRepository= pressNewRepository;
            this._env = env;
        }
        public async Task CreateAsync(PressNewCreateDto entity)
        {
            string rootPath = _env.WebRootPath;
            PressNew pressNew = _mapper.Map<PressNew>(entity);
            if (entity.Image.ContentType != "image/jpeg" && entity.Image.ContentType != "image/png")
            {
                throw new OutOfRangeImageSizeException("Image", "only png or jpeg file!");
            }
            if (entity.Image.Length > 2097152)
            {
                throw new OutOfRangeImageSizeException("Image", "please upload less than 2 mg");
            }
            pressNew.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, entity.Image);
            await _pressNewRepository.CreateAsync(pressNew);
            await _pressNewRepository.CommitAsync();


        }

        public async Task Delete(int id)
        {
            string rootPath = _env.WebRootPath;
            PressNew pressNew = await _pressNewRepository.Get(a => a.Id == id);
            if (pressNew == null) throw new EntityNotFoundException($"The entity with the ID equal " +
                $"to {id} was not found in the database.");
            File.Delete(Path.Combine(rootPath, passPath, pressNew.ImageUrl));
            _pressNewRepository.Delete(pressNew);


        }

        public async Task<PressNew> Get(Expression<Func<PressNew, bool>>? predicate, params string[]? includes)
        {
            return await _pressNewRepository.Get(predicate, includes) is not null ?
                 await _pressNewRepository.Get(predicate, includes) :
                 throw new EntityNotFoundException($"The entity with the ID equal to " +
                 $"{predicate} was not found in the database.");
        }

        public async Task<IEnumerable<PressNew>> GetAll(Expression<Func<PressNew, bool>>? predicate, params string[]? includes)
        {
            return await _pressNewRepository.GetAll(predicate, includes) is not null ?
                await _pressNewRepository.GetAll(predicate, includes) :
                throw new EntityNotFoundException($"The entity with the ID equal to" +
                $" {predicate} was not found in the database.");
        }

        public async Task<PressNew> GetById(int? id)
        {
            return await _pressNewRepository.Get(a => a.Id == id);
        }



        public async Task ToggleDelete(int id)
        {
            var pressNew = await this.GetById(id);
            if (pressNew == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{id} was not found in the database.");
            pressNew.IsDeleted = !pressNew.IsDeleted;
            await _pressNewRepository.CommitAsync();

        }

        public async Task UpdateAsync(PressNewUpdateDto entity)
        {

            string rootPath = _env.WebRootPath;
            var updatedPressNew = await _pressNewRepository.Get(a => a.Id == entity.Id);
            if (updatedPressNew == null) throw new EntityNotFoundException($"The entity with the ID equal to " +
                $"{entity.Id} was not found in the database.");
            updatedPressNew = _mapper.Map(entity, updatedPressNew);


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
                updatedPressNew.ImageUrl = await FileHelper.SaveImage(rootPath, passPath, entity.Image);
            }

            await _pressNewRepository.CommitAsync();

        }

    }
}
